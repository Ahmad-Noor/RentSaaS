import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./financial/financial.page').then(m => m.FinancialPage)
  },
  {
    path: 'expenses',
    loadComponent: () => import('./expenses/expenses.page').then(m => m.ExpensesPage)
  },
  {
    path: 'expenses/new',
    loadComponent: () => import('./add-expense/add-expense.page').then(m => m.AddExpensePage)
  },
  // {
  //   path: 'expenses/:id/edit',
  //   loadComponent: () => import('./edit-expense/edit-expense.page').then(m => m.EditExpensePage)
  // },
  {
    path: 'payments',
    loadComponent: () => import('./payment-list/payment-list.page').then(m => m.PaymentListPage)
  },
  {
    path: 'payments/record',
    loadComponent: () => import('./record-payment/record-payment.page').then(m => m.RecordPaymentPage)
  },
  {
    path: 'reports',
    loadComponent: () => import('./reports/reports.page').then(m => m.ReportsPage)
  },
  {
    path: 'taxes',
    loadComponent: () => import('./taxes/taxes.page').then(m => m.TaxesPage)
  }
] as Routes;