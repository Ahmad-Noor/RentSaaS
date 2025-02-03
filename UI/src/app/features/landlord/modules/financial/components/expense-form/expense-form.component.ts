import { Component, EventEmitter, Input, Output, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ExpenseDetailsFormComponent } from './expense-details-form/expense-details-form.component';
import { PropertySelectorComponent } from './property-selector/property-selector.component';
import { ReceiptUploadComponent } from './receipt-upload/receipt-upload.component';
import { Expense } from '../../types/expense.types';
import { ExpenseFormData } from './models/expense-form.model';
import { initializeExpenseForm } from './utils/form-utils';
import { FormFieldComponent } from "../../../../../../shared/components/form-field/form-field.component";
import { PropertyService } from '../../../properties/services/property.service';

@Component({
  selector: "app-expense-form",
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    ExpenseDetailsFormComponent,
    PropertySelectorComponent,
    ReceiptUploadComponent,
    FormFieldComponent,
  ],
  templateUrl: "./expense-form.component.html",
})
export class ExpenseFormComponent implements OnInit {
  @Input() expense?: Expense;
  @Output() save = new EventEmitter<ExpenseFormData>();

  expenseForm: FormGroup;
  loading = false;
  properties:any[] = [];

  constructor(
    private fb: FormBuilder,
    private _propertyServices: PropertyService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.expenseForm = initializeExpenseForm(fb);


    if(isPlatformBrowser(platformId))
    {
      this.getAllProperties();
    }

  }

  ngOnInit() {
    if (this.expense) {
      this.expenseForm.patchValue({
        type: this.expense.type || "property",
        propertyId: this.expense.propertyId,
        category: this.expense.category,
        expenseType: this.expense.recurring ? "recurring" : "onetime",
        amount: this.expense.amount,
        dueDate: this.expense.dueDate,
        details: this.expense.description,
        isPaid: this.expense.status === "paid",
      });
    }
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
        isPaid: this.expenseForm.value.isPaid,
      };
      this.save.emit(formData);
    }
  }

  getAllProperties() {
    this._propertyServices.getAllProperties().subscribe({
      next: (properties) => {
        this.properties = properties.data;
      },
      error: (properties) => {},
      complete: () => {},
    });
  }

  getFieldError(field: string): string {
    const control = this.expenseForm.get(field);
    if (control?.touched && control.errors) {
      if (control.errors["required"]) {
        return `${field} is required`;
      }
    }
    return "";
  }
}