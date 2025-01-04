import { Component } from '@angular/core';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { ExpenseFormComponent } from '../../components/expense-form/expense-form.component';
import { ExpenseService } from '../../services/expense.service';
import { ExpenseFormData } from '../../components/expense-form/models/expense-form.model';
import { mapFormDataToDTO } from '../../utils/expense-form.utils';

@Component({
  selector: 'app-add-expense-page',
  standalone: true,
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
          <app-expense-form (save)="handleSave($event)" />
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
    const expenseData = mapFormDataToDTO(data);
    this.expenseService.addExpense(expenseData);
    this.router.navigate(['..'], { relativeTo: this.route });
  }
}