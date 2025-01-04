import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Payment } from '../../types/payment.types';

@Component({
    selector: 'app-payment-table',
    imports: [CommonModule],
    template: `
    <table class="w-full">
      <thead>
        <tr class="border-b">
          <th class="text-left py-3 px-4">Date</th>
          <th class="text-left py-3 px-4">Property</th>
          <th class="text-left py-3 px-4">Description</th>
          <th class="text-right py-3 px-4">Amount</th>
          <th class="text-left py-3 px-4">Status</th>
          <th class="text-left py-3 px-4">Actions</th>
        </tr>
      </thead>
      <tbody>
        @for (payment of payments; track payment.id) {
          <tr class="border-b hover:bg-gray-50">
            <td class="py-3 px-4">{{ payment.date | date:'mediumDate' }}</td>
            <td class="py-3 px-4">{{ payment.property }}</td>
            <td class="py-3 px-4">{{ payment.description }}</td>
            <td class="py-3 px-4 text-right">{{ payment.amount | currency }}</td>
            <td class="py-3 px-4">
              <span [class]="getStatusClass(payment.status)">
                {{ payment.status }}
              </span>
            </td>
            <td class="py-3 px-4">
              <div class="flex gap-2">
                <button 
                  class="p-1 text-gray-600 hover:text-blue-600"
                  (click)="onAction.emit({ type: 'view', payment })"
                >
                  <span class="material-icons text-sm">visibility</span>
                </button>
                <button 
                  class="p-1 text-gray-600 hover:text-blue-600"
                  (click)="onAction.emit({ type: 'download', payment })"
                >
                  <span class="material-icons text-sm">download</span>
                </button>
              </div>
            </td>
          </tr>
        }
      </tbody>
    </table>
  `
})
export class PaymentTableComponent {
  @Input() payments: Payment[] = [];
  @Output() onAction = new EventEmitter<{ type: string; payment: Payment }>();

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm';
    const statusClasses: Record<string, string> = {
      'completed': 'bg-green-100 text-green-800',
      'pending': 'bg-yellow-100 text-yellow-800',
      'failed': 'bg-red-100 text-red-800'
    };

    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }
}