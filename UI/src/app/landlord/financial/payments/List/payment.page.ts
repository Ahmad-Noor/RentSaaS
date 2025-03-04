import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterLink, Router, ActivatedRoute } from "@angular/router";
import { Payment } from "../../../../models/payment.types";
import { PaymentService } from "../../../../service/payment.service";
import { ConfirmDialogService } from "../../../../shared/services/confirm-dialog/confirm-dialog.service";

@Component({
  selector: "app-payments-page",
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: "./payments.page.html",
})
export class PaymentsPage implements OnInit {
  payments: Payment[] = [];
  filteredpayments: Payment[] = [];
  @Output() onAction = new EventEmitter<{ type: string; payment: Payment }>();
  
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private paymentservice: PaymentService,
    private confirmDialog: ConfirmDialogService
  ) {}
  
  ngOnInit(): void {
    this.loadPayments();
  }

  loadPayments() {
    this.paymentservice.getAllPayment().subscribe({
      next: (payments) => {
        this.payments = this.processPayments(payments);
        this.filteredpayments = [...this.payments];
      },
      error: (error) => {
        console.error('Failed to load payments:', error);
      }
    });
  }
  
  // Process expense data to ensure consistent format
  processPayments(payments: any[]): Payment[] {
    return payments.map(payment => {
      // Derive status from isPaid and dueDate
      const status = this.determinePaymentStatus(payment);
      
      return {
        ...payment,
        status: status
      };
    });
  }
  
  determinePaymentStatus(payment: any): string {
    if (payment.isPaid) {
      return 'paid';
    }
    
    const dueDate = new Date(payment.dueDate);
    const today = new Date();
    
    return dueDate < today ? 'overdue' : 'pending';
  }

  getStatusClass(status: string): string {
    const baseClasses = "px-2 py-1 rounded-full text-sm capitalize";
    const statusClasses: Record<string, string> = {
      paid: "bg-green-100 text-green-800",
      pending: "bg-yellow-100 text-yellow-800",
      overdue: "bg-red-100 text-red-800",
    };

    return `${baseClasses} ${
      statusClasses[status.toLowerCase()] || "bg-gray-100 text-gray-800"
    }`;
  }

  handleSearch(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    const term = inputElement.value.toLowerCase();
    
    if (!term) {
      this.filteredpayments = [...this.payments];
      return;
    }
    
    this.filteredpayments = this.payments.filter(
      (payment) =>
        (payment.details && payment.details.toLowerCase().includes(term)) ||
        (payment.category && payment.category.toLowerCase().includes(term))
    );
  }

  handleEditAction(payment: Payment) {
    this.router.navigate(['payment'], {
      relativeTo: this.route,
      state: { payment }
    });
  }
  
  async handleDeleteAction(payment: Payment) {
    const isConfirmed = await this.confirmDialog.show({
      title: "Delete payment",
      message: "Are you sure you want to delete this payment?",
      confirmText: "Delete",
      cancelText: "Cancel",
      type: "danger",
    });
    
    if (isConfirmed) {
      this.paymentservice.deletePayment(payment.id).subscribe({
        next: () => {
          // Remove payment from both arrays to update UI
          this.payments = this.payments.filter(e => e.id !== payment.id);
          this.filteredpayments = this.filteredpayments.filter(e => e.id !== payment.id);
        },
        error: (error) => {
          console.error('Error deleting payment:', error);
        }
      });
    }
  }
}