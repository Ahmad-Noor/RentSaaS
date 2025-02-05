import { Component } from '@angular/core';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { PaymentFormComponent } from '../../components/payment-form/payment-form.component';
import { PaymentService } from '../../services/payment.service';
import { CreatePaymentDTO } from '../../types/payment.types';

@Component({
  selector: 'app-record-payment-page',
  standalone: true,
  imports: [RouterLink, PaymentFormComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Record Payment</h1>
          <p class="mt-1 text-gray-600">Record a new payment transaction</p>
        </div>
        <a 
          routerLink="../"
          class="text-gray-600 hover:text-gray-900 flex items-center gap-2"
        >
          <span class="material-icons">arrow_back</span>
          Back to Payments
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-payment-form (onSave)="handleSave($event)" />
        </div>
      </div>
    </div>
  `
})
export class RecordPaymentPage {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private paymentService: PaymentService
  ) {}

  handleSave(data: CreatePaymentDTO): void {
    this.paymentService.addPayment(data);
    this.router.navigate(['..'], { relativeTo: this.route });
  }
}