import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormFieldComponent } from '../../../../shared/components/form-field/form-field.component';
import { PropertySelectorComponent } from './property-selector/property-selector.component';  
import { CreatePaymentDTO } from '../../../../models/payment.types';
import { FileItemComponent } from '../../expenses/expense-add-edit/file-item.component';

@Component({
  selector: 'app-payment-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    FormFieldComponent,
    PropertySelectorComponent,  
    FileItemComponent
  ],
  template: `
<form [formGroup]="paymentForm" (ngSubmit)="handleSubmit()" class="space-y-6">
  <app-property-selector [formGroup]="paymentForm"></app-property-selector> 
 <app-form-field label="Payment Type" id="type">
      <select
        id="type"
        formControlName="type"
        class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
      >
        <option value="">Select type</option>
        <option value="rent">Rent</option>
        <option value="deposit">Security Deposit</option>
        <option value="fee">Fee</option>
        <option value="other">Other</option>
      </select>
    </app-form-field>


  <!-- <app-tenant-selector [formGroup]="paymentForm"></app-tenant-selector>   -->

  <app-form-field label="Tenant" id="tenant"> 
      <select
        id="tenant"
        formControlName="tenant"
        class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
      >
        <option value="">Select tenant</option> 
        <option *ngFor="let tenant of tenants" [value]="tenant.id">
          {{ tenant.name }}
        </option>
      </select>
    </app-form-field>


      
      <div class="grid grid-cols-2 gap-4">
        <app-form-field label="Amount" id="amount">
          <div class="relative">
            <span class="absolute left-3 top-2 text-gray-500">$</span>
            <input
              type="number"
              id="amount"
              formControlName="amount"
              class="w-full pl-8 p-2 border rounded focus:ring-2 focus:ring-blue-500"
              min="0"
              step="0.01"
            >
          </div>
        </app-form-field>

        <app-form-field label="Reference Number" id="reference">
          <input
            type="text"
            id="reference"
            formControlName="reference"
            class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
            placeholder="Optional"
          >
        </app-form-field>
      </div>

      <app-form-field label="Description" id="description">
        <textarea
          id="description"
          formControlName="description"
          rows="3"
          class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
          placeholder="Enter payment details..."
        ></textarea>
      </app-form-field>

      <app-file-item [formGroup]="paymentForm" />

      <div class="flex justify-end gap-4">
        <a
          routerLink="../"
          class="px-4 py-2 text-gray-700 hover:text-gray-900"
        >
          Cancel
        </a>
        <button
          type="submit"
          [disabled]="!paymentForm.valid || loading"
          class="px-4 py-2 text-white bg-blue-600 rounded hover:bg-blue-700 disabled:opacity-50"
        >
          {{ loading ? 'Recording...' : 'Record Payment' }}
        </button>
      </div>
    </form>
  `
})
export class PaymentFormComponent {
  @Output() onSave = new EventEmitter<CreatePaymentDTO>();
  
  paymentForm: FormGroup;
  loading = false;

  tenants = [
    { id: '1', name: 'John Doe' },
    { id: '2', name: 'Jane Smith' },
    { id: '3', name: 'Bob Johnson' }
  ];

  constructor(private fb: FormBuilder) {
    this.paymentForm = this.fb.group({  
     property: ['', Validators.required],
    type: ['', Validators.required],
    tenant: ['', Validators.required],
    amount: ['', [Validators.required, Validators.min(0)]],
    reference: [''],
    description: ['', Validators.required],
    receipts: [[]]

    });
  }

  handleSubmit(): void {
    if (this.paymentForm.valid) {
      this.loading = true;
      const formData: CreatePaymentDTO = {
        property: this.paymentForm.value.property,
        type: this.paymentForm.value.type,
        amount: this.paymentForm.value.amount,
        description: this.paymentForm.value.description,
        tenant: this.paymentForm.value.tenant,
        receipts: this.paymentForm.value.receipts
      };
      this.onSave.emit(formData);
    }
  }
}