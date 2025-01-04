import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Expense } from '../../types/expense.types';

@Component({
    selector: 'app-expense-table',
    imports: [CommonModule],
    template: `
    <table class="w-full">
      <thead>
        <tr class="border-b">
          <th class="text-left py-3 px-4">Date</th>
          <th class="text-left py-3 px-4">Description</th>
          <th class="text-left py-3 px-4">Category</th>
          <th class="text-right py-3 px-4">Amount</th>
          <th class="text-left py-3 px-4">Status</th>
          <th class="text-left py-3 px-4">Actions</th>
        </tr>
      </thead>
      <tbody>
        @for (expense of expenses; track expense.id) {
          <tr class="border-b hover:bg-gray-50">
            <td class="py-3 px-4">{{ expense.date | date:'mediumDate' }}</td>
            <td class="py-3 px-4">
              {{ expense.description }}
              @if (expense.recurring) {
                <span class="ml-2 text-xs bg-blue-100 text-blue-800 px-2 py-1 rounded-full">
                  Recurring
                </span>
              }
            </td>
            <td class="py-3 px-4">
              <span class="capitalize">{{ expense.category }}</span>
            </td>
            <td class="py-3 px-4 text-right">
              {{ expense.amount | currency }}
            </td>
            <td class="py-3 px-4">
              <span [class]="getStatusClass(expense.status)">
                {{ expense.status }}
              </span>
            </td>
            <td class="py-3 px-4">
              <div class="flex gap-2">
                <button 
                  class="p-1 text-gray-600 hover:text-blue-600"
                  (click)="onAction.emit({ type: 'edit', expense })"
                >
                  <span class="material-icons text-sm">edit</span>
                </button>
                <button 
                  class="p-1 text-gray-600 hover:text-red-600"
                  (click)="onAction.emit({ type: 'delete', expense })"
                >
                  <span class="material-icons text-sm">delete</span>
                </button>
              </div>
            </td>
          </tr>
        }
      </tbody>
    </table>
  `
})
export class ExpenseTableComponent {
  @Input() expenses: Expense[] = [];
  @Output() onAction = new EventEmitter<{ type: string; expense: Expense }>();

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm capitalize';
    const statusClasses: Record<string, string> = {
      'paid': 'bg-green-100 text-green-800',
      'pending': 'bg-yellow-100 text-yellow-800',
      'overdue': 'bg-red-100 text-red-800'
    };

    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }
}