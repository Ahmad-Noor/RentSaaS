import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaxPayment } from '../../../../models/tax.types';
import { formatTaxAmount, getStatusClass } from '../../../../utils/tax.utils';

@Component({
  selector: 'app-tax-payment-table',
  standalone: true,
  imports: [CommonModule],
  template: `
    <table class="w-full">
      <thead>
        <tr class="border-b">
          <th class="text-left py-3 px-4">Date</th>
          <th class="text-left py-3 px-4">Type</th>
          <th class="text-left py-3 px-4">Property</th>
          <th class="text-right py-3 px-4">Amount</th>
          <th class="text-left py-3 px-4">Status</th>
          <th class="text-left py-3 px-4">Actions</th>
        </tr>
      </thead>
      <tbody>
        @for (payment of payments; track payment.id) {
          <tr class="border-b">
            <td class="py-3 px-4">{{ payment.date }}</td>
            <td class="py-3 px-4">{{ payment.type }}</td>
            <td class="py-3 px-4">{{ payment.property }}</td>
            <td class="py-3 px-4 text-right">{{ formatAmount(payment.amount) }}</td>
            <td class="py-3 px-4">
              <span [class]="getStatusClass(payment.status)">
                {{ payment.status }}
              </span>
            </td>
            <td class="py-3 px-4">
              <button 
                class="p-2 text-gray-600 hover:text-blue-600 rounded-lg hover:bg-blue-50"
                (click)="onDownload.emit(payment)"
              >
                <span class="material-icons">receipt</span>
              </button>
            </td>
          </tr>
        }
      </tbody>
    </table>
  `
})
export class TaxPaymentTableComponent {
  @Input() payments: TaxPayment[] = [];
  @Output() onDownload = new EventEmitter<TaxPayment>();

  protected formatAmount = formatTaxAmount;
  protected getStatusClass = getStatusClass;
}