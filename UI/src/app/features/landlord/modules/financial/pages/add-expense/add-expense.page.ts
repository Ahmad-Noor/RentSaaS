import { Component } from '@angular/core';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { ExpenseFormComponent } from '../../components/expense-form/expense-form.component';
import { ExpenseService } from '../../services/expense.service';
import { ExpenseFormData } from '../../components/expense-form/models/expense-form.model';
import { initializeExpenseForm, mapFormDataToDTO } from '../../utils/expense-form.utils';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ExpenseDetailsFormComponent } from '../../components/expense-form/expense-details-form/expense-details-form.component';
import { ExpenseTypeSelectorComponent } from '../../components/expense-form/expense-type-selector/expense-type-selector.component';
import { PropertySelectorComponent } from '../../components/expense-form/property-selector.component';
import { ReceiptUploadComponent } from '../../components/expense-form/receipt-upload.component';
import { Expense } from '../../types/expense.types';

@Component({
  selector: 'app-add-expense-page',
  standalone: true,
  imports: [RouterLink, ExpenseFormComponent,   
     CommonModule,
      ReactiveFormsModule,
      ExpenseDetailsFormComponent,
      ExpenseTypeSelectorComponent,
      PropertySelectorComponent,
      ReceiptUploadComponent],
  templateUrl: './add-expense.page.html',
  styleUrls: ['./add-expense.page.css']
})
export class AddExpensePage {
expense?: Expense;
   

  expenseForm: FormGroup;
  loading = false;




  constructor(private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private expenseService: ExpenseService
  ) {

    this.expenseForm = initializeExpenseForm(fb);
  }





  OnInite() {
    if (this.expense) {
      this.expenseForm.patchValue({
        PaymentSchedule: this.expense.type || 'PaymentSchedule',
        propertyId: this.expense.propertyId,
        category: this.expense.category,
        expenseType: this.expense.recurring ? 'recurring' : 'onetime',
        amount: this.expense.amount,
        dueDate: this.expense.dueDate,
        details: this.expense.description,
        isPaid: this.expense.status === 'paid'
      });
    }
  }



  handleSave(data: ExpenseFormData): void {
    const expenseData = mapFormDataToDTO(data);
    this.expenseService.addExpense(expenseData);
    this.router.navigate(['..'], { relativeTo: this.route });
  }

  handleSubmit(): void {
    if (this.expenseForm.valid) {
      this.loading = true;
      const formData: ExpenseFormData = {
        type: this.expenseForm.value.type,
        propertyId: this.expenseForm.value.propertyId,
        category: this.expenseForm.value.category,
        expenseType: this.expenseForm.value.expenseType,
        amount: this.expenseForm.value.amount,
        dueDate: this.expenseForm.value.dueDate,
        details: this.expenseForm.value.details,
        receipts: this.expenseForm.value.receipts || [],
        isPaid: this.expenseForm.value.isPaid
      };
  
    }
  }




}