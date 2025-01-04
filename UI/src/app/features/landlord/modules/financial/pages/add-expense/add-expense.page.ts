import { Component } from '@angular/core';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { ExpenseFormComponent } from '../../components/expense-form/expense-form.component';
import { ExpenseService } from '../../services/expense.service';
import { ExpenseFormData } from '../../components/expense-form/models/expense-form.model';

@Component({
    selector: 'app-add-expense-page',
    imports: [RouterLink, ExpenseFormComponent],
    template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <h1 class="text-2xl font-semibold">Add New Expense</h1>
        <a 
          routerLink="../"
          class="text-gray-600 hover:text-gray-900 flex items-center gap-2"
        >
          <span class="material-icons">arrow_back</span>
          Back to Expenses
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-expense-form (onSave)="handleSave($event)" />
        </div>
      </div>
    </div>
  `
})
export class AddExpensePage {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private expenseService: ExpenseService
  ) {}

  handleSave(data: ExpenseFormData): void {
    this.expenseService.addExpense({
      description: data.details || '',
      amount: data.amount,
      category: data.category,
      date: new Date().toISOString(),
      dueDate: data.dueDate,
      propertyId: data.type === 'property' ? Number(data.propertyId) : undefined,
      recurring: data.expenseType === 'recurring',
      vendor: data.vendor,
      type: data.type
    });
    
    this.router.navigate(['..'], { relativeTo: this.route });
  }
}