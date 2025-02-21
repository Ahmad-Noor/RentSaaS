import { Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { BehaviorSubject, Observable, map } from "rxjs";
import { Expense, CreateExpenseDTO } from "../models/expense.types";
import { MOCK_EXPENSES } from "../landlord/financial/data/mock-expenses";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { isPlatformBrowser } from "@angular/common";
import { Constant } from "../constants";
import { UserService } from "./user.service";

@Injectable({
  providedIn: "root",
})
export class ExpenseService {
  baseUrl: string = environment.apiUrl;
  headers!: HttpHeaders;
  private expenses = new BehaviorSubject<Expense[]>(MOCK_EXPENSES);

  constructor(
    private _httpClient: HttpClient,
    private userService: UserService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.expenses = new BehaviorSubject<Expense[]>([]);
    this.initializationHeader();
  }
 

  initializationHeader(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.headers = new HttpHeaders({
        "X-OrganizationId": `${this.userService.getCurrentOrganizationId()}`,
        Authorization: `Bearer ${this.userService.getToken()}`,
      });
    } else {
      this.headers = new HttpHeaders();
    }
  }
 
  getExpenses(): Observable<Expense[]> {
    return this.expenses.asObservable();
  }

  getExpenseById(id: number): Observable<Expense | undefined> {
    return this.expenses.pipe(
      map((expenses) => expenses.find((expense) => expense.id === id))
    );
  }

  addExpense(expenseData: CreateExpenseDTO): void {
    const currentExpenses = this.expenses.getValue();
    const newExpense: Expense = {
      ...expenseData,
      id: Math.max(0, ...currentExpenses.map((e) => e.id)) + 1,
      status: "pending",
    };

    this.expenses.next([...currentExpenses, newExpense]);
  }

  updateExpense(id: number, updates: Partial<Expense>): void {
    const currentExpenses = this.expenses.getValue();
    const updatedExpenses = currentExpenses.map((expense) =>
      expense.id === id ? { ...expense, ...updates } : expense
    );
    this.expenses.next(updatedExpenses);
  }

  deleteExpense(id: number): Observable<any> {
    const currentExpenses = this.expenses.getValue();
    this.expenses.next(currentExpenses.filter((expense) => expense.id !== id));
    
    return this._httpClient.delete(`${this.baseUrl}api/Expense/${id}`, {
      headers: this.headers
    });
  }

  getAllExpenses(): Observable<any> {
    console.log(this.headers);
    return this._httpClient.get(`${this.baseUrl}api/Expense/getall`, {
      headers: this.headers,
    });
  }

  add(formData: FormData): Observable<any> {
    return this._httpClient.post(
     `${this.baseUrl}api/Expense/add`,
      formData,
      {
        headers: this.headers,
      }
    );
  }
}
