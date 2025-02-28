import { Observable} from "rxjs";
import { map } from 'rxjs/operators'; 
import { Injectable } from "@angular/core";
import { UserService } from "./user.service";
import { Expense } from "../models/expense.types";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { APIResponse } from "../models/api-response.types";

@Injectable({
  providedIn: "root",
})
export class ExpenseService {
  apiUrl: string = `${environment.apiUrl}api/expense`;
  private headers!: HttpHeaders;

  constructor(private http: HttpClient, private _userService: UserService) {
    this.headers = new HttpHeaders({
    //  "Content-Type": "application/json",
      "X-OrganizationId": this._userService.getCurrentOrganizationId() || "",
      Authorization: `Bearer ${this._userService.getToken()}`,
    });
   }

  getAllExpenses(): Observable<Expense[]> {
    return this.http
      .get<APIResponse<Expense[]>>(this.apiUrl, { headers: this.headers })
      .pipe(
        map(response => response.data)
      );
  }
    getExpenseById(id: number): Observable<Expense> {

    return this.http.get<Expense>(`${this.apiUrl}/${id}`, {headers: this.headers});
  }

  addExpense(expenseData: Expense, files?: File[]): Observable<Expense> {
    const formData = new FormData();
    
    // Add expense data fields
    for (const key in expenseData) {
      if (expenseData.hasOwnProperty(key) && (expenseData as any)[key] !== null) {
        // Handle different data types appropriately
        if (key === 'receipts' && Array.isArray((expenseData as any)[key])) {
          continue; // Skip the receipts array as we'll handle files separately
        } else if (typeof (expenseData as any)[key] === 'object') {
          formData.append(key, JSON.stringify((expenseData as any)[key]));
        } else {
          formData.append(key, (expenseData as any)[key].toString());
        }
      }
    }
    
    // Add receipt files
    if (files && files.length > 0) {
      files.forEach(file => {
        formData.append('ReceiptsFiles', file, file.name);
      });
    }
    
    // Create headers without Content-Type (browser will set it automatically for FormData)
    const headers = new HttpHeaders({
      "X-OrganizationId": this._userService.getCurrentOrganizationId() || "",
      Authorization: `Bearer ${this._userService.getToken()}`,
    });
    
    return this.http.post<Expense>(this.apiUrl, formData, {headers});
  }
  updateExpense(expenseId: string, expenseData: Expense, files?: File[]): Observable<Expense> {
    const formData = new FormData();
    
    // Add expense data fields
    for (const key in expenseData) {
      if (expenseData.hasOwnProperty(key) && (expenseData as any)[key] !== null) {
        if (typeof (expenseData as any)[key] === 'object' && !((expenseData as any)[key] instanceof File)) {
          formData.append(key, JSON.stringify((expenseData as any)[key]));
        } else {
          formData.append(key, (expenseData as any)[key]);
        }
      }
    }
    
    // Add receipt files
    if (files && files.length > 0) {
      files.forEach(file => {
        formData.append('ReceiptsFiles', file, file.name);
      });
    }
    
    const headers = new HttpHeaders({
      "X-OrganizationId": this._userService.getCurrentOrganizationId() || "",
      Authorization: `Bearer ${this._userService.getToken()}`,
    });
    
    return this.http.put<Expense>(`${this.apiUrl}/${expenseId}`, formData, {headers});
  }
  deleteExpense(expenseId: string): Observable<Expense> {
    return this.http.delete<Expense>(`${this.apiUrl}/${expenseId}`, {headers: this.headers});
  }
}
