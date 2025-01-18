import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { ExpenseTableComponent } from '../../components/expense-table/expense-table.component';
import { ActionBarComponent } from '../../../../../../shared/components/action-bar/action-bar.component';
import { ExpenseService } from '../../services/expense.service';
import { Expense } from '../../types/expense.types';
import { ConfirmDialogService } from '../../../../../../shared/services/confirm-dialog/confirm-dialog.service';

@Component({
  selector: 'app-expenses-page',
  standalone: true,
  imports: [CommonModule, RouterLink, ExpenseTableComponent, ActionBarComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Expenses</h1>
          <p class="mt-1 text-gray-600">Track and manage your property expenses</p>
        </div>
        <a 
          routerLink="new"
          class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors"
        >
          <span class="material-icons text-sm">add</span>
          Add Expense
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-action-bar 
            searchPlaceholder="Search expenses" 
            (onSearch)="handleSearch($event)"
          />
          
          <app-expense-table
            [expenses]="filteredExpenses"
            (onAction)="handleAction($event)"
          />
        </div>
      </div>
    </div>
  `
})
export class ExpensesPage {
  expenses: Expense[] = [];
  filteredExpenses: Expense[] = [];

  constructor(
    private router: Router,
    private expenseService: ExpenseService,
    private confirmDialog: ConfirmDialogService
  ) {
    this.expenseService.getExpenses().subscribe(expenses => {
      this.expenses = expenses;
      this.filteredExpenses = expenses;
    });
  }

  handleSearch(term: string): void {
    this.filteredExpenses = this.expenses.filter(expense => 
      expense.description.toLowerCase().includes(term.toLowerCase()) ||
      expense.category.toLowerCase().includes(term.toLowerCase())
    );
  }

  async handleAction(action: { type: string; expense: Expense }): Promise<void> {
    switch (action.type) {
      case 'edit':
        this.router.navigate(['landlord', 'financial', 'expenses', action.expense.id, 'edit']);
        break;
        
      case 'delete':
        const confirmed = await this.confirmDialog.show({
          title: 'Delete Expense',
          message: 'Are you sure you want to delete this expense?',
          confirmText: 'Delete',
          cancelText: 'Cancel',
          type: 'danger'
        });
        
        if (confirmed) {
          this.expenseService.deleteExpense(action.expense.id);
        }
        break;
    }
  }
}