import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaxPaymentTableComponent } from './tax-payment-table.component';
import { TaxPayment } from '../../../../models/tax.types';

@Component({
  selector: 'app-tax-payments',
  standalone: true,
  imports: [CommonModule, TaxPaymentTableComponent],
  template: `
    <div class="bg-white rounded-lg shadow-sm">
      <div class="p-4 border-b">
        <h2 class="text-lg font-semibold">Recent Tax Payments</h2>
      </div>
      <div class="p-4">
        <app-tax-payment-table 
          [payments]="payments"
          (onDownload)="downloadReceipt($event)"
        />
      </div>
    </div>
  `
})
export class TaxPaymentsComponent {
  payments: TaxPayment[] = [
    {
      id: 1,
      date: '2024-01-15',
      type: 'Property Tax',
      property: 'Sunset Apartments',
      amount: 6125.00,
      status: 'paid'
    },
    {
      id: 2,
      date: '2024-01-15',
      type: 'Income Tax',
      property: 'All Properties',
      amount: 4687.50,
      status: 'pending'
    },
    {
      id: 3,
      date: '2023-12-15',
      type: 'Property Tax',
      property: 'Downtown Lofts',
      amount: 4250.00,
      status: 'paid'
    }
  ];

  downloadReceipt(payment: TaxPayment): void {
    // TODO: Implement receipt download
    console.log('Downloading receipt for payment:', payment);
  }
}