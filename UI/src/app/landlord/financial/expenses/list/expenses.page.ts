import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterLink, Router, ActivatedRoute } from "@angular/router";
import { Expense } from "../../../../models/expense.types";
import { ExpenseService } from "../../../../service/expense.service";
import { ConfirmDialogService } from "../../../../shared/services/confirm-dialog/confirm-dialog.service";

@Component({
  selector: "app-expenses-page",
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: "./expenses.page.html",
})
export class ExpensesPage implements OnInit {
  expenses: Expense[] = [];
  filteredExpenses: Expense[] = [];
  @Output() onAction = new EventEmitter<{ type: string; expense: Expense }>();
  
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private expenseService: ExpenseService,
    private confirmDialog: ConfirmDialogService
  ) {}
  
  ngOnInit(): void {
    this.loadExpenses();
  }

  loadExpenses() {
    this.expenseService.getAllExpenses().subscribe({
      next: (expenses) => {
        this.expenses = this.processExpenses(expenses);
        this.filteredExpenses = [...this.expenses];
      },
      error: (error) => {
        console.error('Failed to load expenses:', error);
      }
    });
  }
  
  // Process expense data to ensure consistent format
  processExpenses(expenses: any[]): Expense[] {
    return expenses.map(expense => {
      // Derive status from isPaid and dueDate
      const status = this.determineExpenseStatus(expense);
      
      return {
        ...expense,
        status: status
      };
    });
  }
  
  determineExpenseStatus(expense: any): string {
    if (expense.isPaid) {
      return 'paid';
    }
    
    const dueDate = new Date(expense.dueDate);
    const today = new Date();
    
    return dueDate < today ? 'overdue' : 'pending';
  }

  getStatusClass(status: string): string {
    const baseClasses = "px-2 py-1 rounded-full text-sm capitalize";
    const statusClasses: Record<string, string> = {
      paid: "bg-green-100 text-green-800",
      pending: "bg-yellow-100 text-yellow-800",
      overdue: "bg-red-100 text-red-800",
    };

    return `${baseClasses} ${
      statusClasses[status.toLowerCase()] || "bg-gray-100 text-gray-800"
    }`;
  }

  handleSearch(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    const term = inputElement.value.toLowerCase();
    
    if (!term) {
      this.filteredExpenses = [...this.expenses];
      return;
    }
    
    this.filteredExpenses = this.expenses.filter(
      (expense) =>
        (expense.details && expense.details.toLowerCase().includes(term)) ||
        (expense.category && expense.category.toLowerCase().includes(term))
    );
  }

  handleEditAction(expense: Expense) {
    this.router.navigate(['add'], {
      relativeTo: this.route,
      state: { expense }
    });
  }
  
  async handleDeleteAction(expense: Expense) {
    const isConfirmed = await this.confirmDialog.show({
      title: "Delete Expense",
      message: "Are you sure you want to delete this expense?",
      confirmText: "Delete",
      cancelText: "Cancel",
      type: "danger",
    });
    
    if (isConfirmed) {
      this.expenseService.deleteExpense(expense.id).subscribe({
        next: () => {
          // Remove expense from both arrays to update UI
          this.expenses = this.expenses.filter(e => e.id !== expense.id);
          this.filteredExpenses = this.filteredExpenses.filter(e => e.id !== expense.id);
        },
        error: (error) => {
          console.error('Error deleting expense:', error);
        }
      });
    }
  }
}