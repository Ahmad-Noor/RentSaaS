import { Observable} from "rxjs";
import { UserService } from "./user.service";
import { Injectable } from "@angular/core";
import { Expense } from "../models/expense.types";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { map } from 'rxjs/operators'; 
import { APIResponse } from "../models/api-response.types";

@Injectable({
  providedIn: "root",
})
export class ExpenseService {
  apiUrl: string = `${environment.apiUrl}api/expense`;
    headers!: HttpHeaders;

  constructor(private http: HttpClient, private userService: UserService) {
    this.headers = new HttpHeaders({
      "X-OrganizationId": `${this.userService.getCurrentOrganizationId()}`,
      Authorization: `Bearer ${this.userService.getToken()}`,
    });
  }

  // getAllExpenses(): Observable<Expense[]> {
  //   return this.http.get<Expense[]>(this.apiUrl, {headers: this.headers});
  // }

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
    console.log(data);
    return this.http.post<Expense>(this.apiUrl, data, {headers: this.headers});
  }
 
  updateExpense(id: string, data: Expense): Observable<Expense> {
    return this.http.put<Expense>(`${this.apiUrl}/${id}`, data, {headers: this.headers});
  }

  deleteExpense(id: string): Observable<Expense> {
    return this.http.delete<Expense>(`${this.apiUrl}/${id}`, {headers: this.headers});
  }
}
