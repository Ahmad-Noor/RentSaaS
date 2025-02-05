import { Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { BehaviorSubject, Observable, map } from "rxjs";
import { Expense, CreateExpenseDTO } from "../types/expense.types";
import { MOCK_EXPENSES } from "../data/mock-expenses";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "../../../../../environments/environment";
import { NameConstnt } from "../../../../../assets/nameConstant";
import { isPlatformBrowser } from "@angular/common";

@Injectable({
  providedIn: "root",
})
export class ExpenseService {
  baseUrl: string = environment.apiUrl;
  headers!: HttpHeaders;
  private expenses = new BehaviorSubject<Expense[]>(MOCK_EXPENSES);

  initalizationHeader(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.headers = new HttpHeaders({
        "X-OrganizationId": `${localStorage.getItem(
          NameConstnt.organizationId
        )}`,
        Authorization: `Bearer ${localStorage.getItem(NameConstnt.token)}`,
      });
    } else {
      this.headers = new HttpHeaders();
    }
  }

  constructor(
    private _httpclint: HttpClient,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.expenses = new BehaviorSubject<Expense[]>([]);
    this.initalizationHeader();
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

  deleteExpense(id: number): void {
    const currentExpenses = this.expenses.getValue();
    this.expenses.next(currentExpenses.filter((expense) => expense.id !== id));
  }

  getAllExpenses(): Observable<any> {
    console.log(this.headers);
    return this._httpclint.get(`${this.baseUrl}api/Expense/getall`, {
      headers: this.headers,
    });
  }


  
}
