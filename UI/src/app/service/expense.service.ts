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

  addExpense(data: Expense): Observable<Expense> {
    let headers = this.headers;
    console.log("this.headers", headers);
    return this.http.post<Expense>(this.apiUrl, data, {headers: this.headers});
  }
 
  updateExpense(id: string, data: Expense): Observable<Expense> {
    return this.http.put<Expense>(`${this.apiUrl}/${id}`, data, {headers: this.headers});
  }

  deleteExpense(id: string): Observable<Expense> {
    return this.http.delete<Expense>(`${this.apiUrl}/${id}`, {headers: this.headers});
  }
}
