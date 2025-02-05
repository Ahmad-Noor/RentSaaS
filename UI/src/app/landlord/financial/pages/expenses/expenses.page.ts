import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { ExpenseService } from '../../services/expense.service';
import { Expense } from '../../types/expense.types';
import { ConfirmDialogService } from '../../../../shared/services/confirm-dialog/confirm-dialog.service';

@Component({
  selector: 'app-expenses-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './expenses.page.html',
  styleUrls: ['./expenses.page.css']
})
export class ExpensesPage {
  expenses: any[] = [];
  filteredExpenses: any[] = [];
  @Output() onAction = new EventEmitter<{ type: string; expense: Expense }>();
  dataSource: any;

  constructor(
    private router: Router,
    private expenseService: ExpenseService,
    private confirmDialog: ConfirmDialogService
  ) {
    this.expenseService.getExpenses().subscribe(expenses => {
      this.expenses = expenses;
      this.filteredExpenses = expenses; // Initialize with all expenses
    });


this.expenseService.getAllExpenses().subscribe({
  next: (expenses) => {
    console.log(expenses);
    this.expenses = expenses;
    this.filteredExpenses = expenses;
  }
}
)

    
  }

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm capitalize';
    const statusClasses: Record<string, string> = {
      'paid': 'bg-green-100 text-green-800',
      'pending': 'bg-yellow-100 text-yellow-800',
      'overdue': 'bg-red-100 text-red-800'
    };

    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }

  handleSearch(event: Event): void {
    const inputElement = event.target as HTMLInputElement; // Cast to HTMLInputElement
    const term = inputElement.value;
    this.filteredExpenses = this.expenses.filter(expense =>
      expense.description.toLowerCase().includes(term.toLowerCase()) ||
      expense.category.toLowerCase().includes(term.toLowerCase())
    );
  }

  async handleAction(action: { type: string; expense: Expense }): Promise<void> {
    switch (action.type) {
      case 'edit':
        this.editExpense(action.expense);
        break;

      case 'delete':
        this.deleteExpense(action.expense);
        break;
    }
  }

  // Implement the editExpense method
  editExpense(expense: Expense): void {
    this.router.navigate(['landlord', 'financial', 'expenses', expense.id, 'edit']);
  }

  // Implement the deleteExpense method
  async deleteExpense(expense: Expense): Promise<void> {
    const confirmed = await this.confirmDialog.show({
      title: 'Delete Expense',
      message: 'Are you sure you want to delete this expense?',
      confirmText: 'Delete',
      cancelText: 'Cancel',
      type: 'danger'
    });
    if (confirmed) {
      await this.expenseService.deleteExpense(expense.id);
      // After deletion, update the filtered expenses
      this.filteredExpenses = this.filteredExpenses.filter(exp => exp.id !== expense.id);
    }
  }

  // Implement the filterData method
  filterData(value: string, filterType: string): void {
    if (filterType === 'description') {
      this.filteredExpenses = this.expenses.filter(expense => 
        expense.description.toLowerCase().includes(value.toLowerCase())
      );
    } else if (filterType === 'category') {
      this.filteredExpenses = this.expenses.filter(expense => 
        expense.category.toLowerCase().includes(value.toLowerCase())
      );
    }
  }
}
