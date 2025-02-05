import { Component, OnInit } from '@angular/core';
import { RouterLink, ActivatedRoute, Router } from '@angular/router';
import { ExpenseFormComponent } from '../../components/expense-form/expense-form.component';
import { ExpenseService } from '../../services/expense.service';
import { ExpenseFormData } from '../../components/expense-form/models/expense-form.model';
import { CreateExpenseDTO, Expense } from '../../types/expense.types';

@Component({
  selector: 'app-edit-expense-page',
  standalone: true,
  imports: [RouterLink, ExpenseFormComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <h1 class="text-2xl font-semibold">Edit Expense</h1>
        <a 
          routerLink="../../"
          class="text-gray-600 hover:text-gray-900 flex items-center gap-2"
        >
          <span class="material-icons">arrow_back</span>
          Back to Expenses
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          @if (expense) {
            <app-expense-form 
              [expense]="expense"
              (save)="handleSave($event)"
            />
          }
        </div>
      </div>
    </div>
  `
})
export class EditExpensePage implements OnInit {
  expense?: Expense;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private expenseService: ExpenseService
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.expenseService.getExpenseById(id).subscribe(expense => {
      this.expense = expense;
    });
  }

  handleSave(data: ExpenseFormData) {
    if (this.expense) {
      const updateData: Partial<CreateExpenseDTO> = {
        description: data.details,
        amount: data.amount,
        category: data.category,
        dueDate: data.dueDate,
        propertyId: data.type === 'property' ? Number(data.propertyId) : undefined,
        recurring: data.expenseType === 'recurring',
        type: data.type
      };

      this.expenseService.updateExpense(this.expense.id, updateData);
      this.router.navigate(['../../'], { relativeTo: this.route });
    }
  }
}