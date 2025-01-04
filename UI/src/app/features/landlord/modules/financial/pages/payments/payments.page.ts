import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { PaymentTableComponent } from '../../components/payment-table/payment-table.component';
import { ActionBarComponent } from '../../../../../../shared/components/action-bar/action-bar.component';
import { PaymentService } from '../../services/payment.service';
import { Payment } from '../../types/payment.types';

@Component({
  selector: 'app-payments-page',
  standalone: true,
  imports: [CommonModule, RouterLink, PaymentTableComponent, ActionBarComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Payments</h1>
          <p class="mt-1 text-gray-600">Manage and track all property-related payments</p>
        </div>
        <a 
          routerLink="new"
          class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors"
        >
          <span class="material-icons text-sm">add</span>
          Record Payment
        </a>
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
export class PaymentsPage {
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
      payment.property.toLowerCase().includes(term.toLowerCase())
    );
  }

  handleAction(action: { type: string; payment: Payment }): void {
    switch (action.type) {
      case 'view':
        // TODO: Implement view payment details
        break;
      case 'download':
        // TODO: Implement payment receipt download
        break;
    }
  }
}