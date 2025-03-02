import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./financial/financial.page').then(m => m.FinancialPage)
  },
  {
    path: 'expenses',
    loadComponent: () => import('./expenses/list/expenses.page').then(m => m.ExpensesPage)
  }, 
  {
    path: 'address',
    loadComponent: () => import('./address/address.component').then(m => m.AddressComponent)
  }, 
  {
    path: 'countries',
    loadComponent: () => import('./countries/countries.component').then(m => m.CountriesComponent)
  },
 
  {
    path: 'expenses/expense',
    loadComponent: () => import('./expenses/expense-add-edit/expense-add-edit.page').then(m => m.ExpenseAddEditPage)
  },

  // {
  //   path: 'expenses/:id/edit',
  //   loadComponent: () => import('./edit-expense/edit-expense.page').then(m => m.EditExpensePage)
  // },
  {
    path: 'payments',
    loadComponent: () => import('./payments/payment-list/payment-list.page').then(m => m.PaymentListPage)
  },
  {
    path: 'payments/record',
    loadComponent: () => import('./payments/record-payment/record-payment.page').then(m => m.RecordPaymentPage)
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