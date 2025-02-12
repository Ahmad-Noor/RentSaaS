import { Component } from '@angular/core';
import { RouterLink, Router, ActivatedRoute } from '@angular/router'; 
import { ExpenseService } from '../services/expense.service'; 
import { ExpenseFormData } from '../types/expense-form.types';
import { mapFormDataToDTO } from '../utils/expense-form.utils'; 
import { AddComponent } from '../expenses/add/add.component'; 

@Component({
  selector: 'app-add-expense-page',
  standalone: true,
  imports: [RouterLink, AddComponent],
  templateUrl: './add-expense.page.html',
  styleUrls: ['./add-expense.page.css']
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