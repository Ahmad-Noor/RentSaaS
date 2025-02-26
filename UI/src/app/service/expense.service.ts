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
      "Content-Type": "application/json",
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

  addExpense(expenseData: FormData): Observable<any> { 
    return this.http.post<any>(this.apiUrl, expenseData, {headers: this.headers});
  }
 
  updateExpense(expenseId: string, expenseData: FormData): Observable<Expense> {
    return this.http.put<Expense>(`${this.apiUrl}/${expenseId}`, expenseData, {headers: this.headers});
  }

  deleteExpense(expenseId: string): Observable<Expense> {
    return this.http.delete<Expense>(`${this.apiUrl}/${expenseId}`, {headers: this.headers});
  }
}
