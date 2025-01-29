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
  templateUrl: './expenses.page.html',
  styleUrls: ['./expenses.page.css']
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