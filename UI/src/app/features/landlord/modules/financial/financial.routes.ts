import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./pages/financial/financial.page').then(m => m.FinancialPage)
  },
  {
    path: 'expenses',
    loadComponent: () => import('./pages/expenses/expenses.page').then(m => m.ExpensesPage)
  },
  {
    path: 'expenses/new',
    loadComponent: () => import('./pages/add-expense/add-expense.page').then(m => m.AddExpensePage)
  },
  {
    path: 'expenses/:id/edit',
    loadComponent: () => import('./pages/edit-expense/edit-expense.page').then(m => m.EditExpensePage)
  },
  {
    path: 'payments',
    loadComponent: () => import('./pages/payment-list/payment-list.page').then(m => m.PaymentListPage)
  },
  {
    path: 'payments/record',
    loadComponent: () => import('./pages/record-payment/record-payment.page').then(m => m.RecordPaymentPage)
  },
  {
    path: 'reports',
    loadComponent: () => import('./pages/reports/reports.page').then(m => m.ReportsPage)
  },
  {
    path: 'taxes',
    loadComponent: () => import('./pages/taxes/taxes.page').then(m => m.TaxesPage)
  }
] as Routes;