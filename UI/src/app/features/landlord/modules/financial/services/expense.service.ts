import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { Expense, CreateExpenseDTO } from '../types/expense.types';
import { MOCK_EXPENSES } from '../data/mock-expenses';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  private expenses = new BehaviorSubject<Expense[]>(MOCK_EXPENSES);

  getExpenses(): Observable<Expense[]> {
    return this.expenses.asObservable();
  }

  getExpenseById(id: number): Observable<Expense | undefined> {
    return this.expenses.pipe(
      map(expenses => expenses.find(expense => expense.id === id))
    );
  }

  addExpense(expenseData: CreateExpenseDTO): void {
    const currentExpenses = this.expenses.getValue();
    const newExpense: Expense = {
      ...expenseData,
      id: Math.max(0, ...currentExpenses.map(e => e.id)) + 1,
      status: 'pending'
    };
    
    this.expenses.next([...currentExpenses, newExpense]);
  }

  updateExpense(id: number, updates: Partial<Expense>): void {
    const currentExpenses = this.expenses.getValue();
    const updatedExpenses = currentExpenses.map(expense =>
      expense.id === id ? { ...expense, ...updates } : expense
    );
    this.expenses.next(updatedExpenses);
  }

  deleteExpense(id: number): void {
    const currentExpenses = this.expenses.getValue();
    this.expenses.next(currentExpenses.filter(expense => expense.id !== id));
  }
}