import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { PaymentTableComponent } from '../../components/payment-table/payment-table.component';
import { ActionBarComponent } from '../../../../../../shared/components/action-bar/action-bar.component';
import { PaymentService } from '../../services/payment.service';
import { Payment } from '../../types/payment.types';

@Component({
    selector: 'app-payment-list-page',
    imports: [CommonModule, RouterLink, PaymentTableComponent, ActionBarComponent],
    template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Payment List</h1>
          <p class="mt-1 text-gray-600">View and manage all payment transactions</p>
        </div>
        <div class="flex gap-2">
          <a 
            routerLink="record"
            class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors"
          >
            <span class="material-icons text-sm">add</span>
            Record Payment
          </a>
          <button 
            class="border border-gray-300 px-4 py-2 rounded flex items-center gap-2 hover:bg-gray-50"
            (click)="exportPayments()"
          >
            <span class="material-icons text-sm">download</span>
            Export
          </button>
        </div>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-action-bar 
            searchPlaceholder="Search payments" 
            (onSearch)="handleSearch($event)"
          />
          
          <app-payment-table
            [payments]="filteredPayments"
            (onAction)="handleAction($event)"
          />
        </div>
      </div>
    </div>
  `
})
export class PaymentListPage {
  payments: Payment[] = [];
  filteredPayments: Payment[] = [];

  constructor(private paymentService: PaymentService) {
    this.paymentService.getPayments().subscribe(payments => {
      this.payments = payments;
      this.filteredPayments = payments;
    });
  }

  handleSearch(term: string): void {
    this.filteredPayments = this.payments.filter(payment => 
      payment.description.toLowerCase().includes(term.toLowerCase()) ||
      payment.property.toLowerCase().includes(term.toLowerCase()) ||
      payment.tenant?.toLowerCase().includes(term.toLowerCase())
    );
  }

  handleAction(action: { type: string; payment: Payment }): void {
    switch (action.type) {
      case 'view':
        // TODO: Implement view payment details
        console.log('View payment:', action.payment);
        break;
      case 'download':
        this.downloadReceipt(action.payment);
        break;
    }
  }

  exportPayments(): void {
    // TODO: Implement payment export functionality
    console.log('Exporting payments...');
  }

  private downloadReceipt(payment: Payment): void {
    // TODO: Implement receipt download
    console.log('Downloading receipt for payment:', payment);
  }
}