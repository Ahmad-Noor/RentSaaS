import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Payment, CreatePaymentDTO } from '../types/payment.types';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private payments = new BehaviorSubject<Payment[]>([
    {
      id: 1,
      date: '2024-01-15',
      property: 'Sunset Apartments',
      description: 'Monthly Rent Payment',
      amount: 1500.00,
      status: 'completed',
      type: 'rent',
      tenant: 'John Doe',
      reference: 'PAY-2024011501'
    },
    {
      id: 2,
      date: '2024-01-20',
      property: 'Downtown Lofts',
      description: 'Security Deposit',
      amount: 2000.00,
      status: 'pending',
      type: 'deposit',
      tenant: 'Jane Smith',
      reference: 'PAY-2024012001'
    }
  ]);

  getPayments(): Observable<Payment[]> {
    return this.payments.asObservable();
  }

  addPayment(paymentData: CreatePaymentDTO): void {
    const currentPayments = this.payments.getValue();
    const newPayment: Payment = {
      ...paymentData,
      id: Math.max(0, ...currentPayments.map(p => p.id)) + 1,
      date: new Date().toISOString(),
      status: 'pending',
      reference: `PAY-${Date.now()}`
    };
    
    this.payments.next([...currentPayments, newPayment]);
  }
}