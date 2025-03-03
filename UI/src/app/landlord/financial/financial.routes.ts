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
    path: 'payments/payment',
    loadComponent: () => import('./payments/payment-add-edit/payment-add-edit.component').then(m => m.PaymentAddEditComponent)
  },
  {
    path: 'payments',
    loadComponent: () => import('./payments/List/payment.page').then(m => m.PaymentsPage)
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