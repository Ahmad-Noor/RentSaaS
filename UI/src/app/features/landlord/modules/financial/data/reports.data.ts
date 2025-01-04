import { ReportCategory } from '../types/reports.types';

export const REPORT_CATEGORIES: ReportCategory[] = [
  {
    id: 'income',
    name: 'Income Reports',
    icon: 'payments',
    reports: [
      {
        id: 'rent-roll',
        name: 'Rent Roll',
        description: 'Monthly rent collection and outstanding balances',
        type: 'financial',
        format: 'excel'
      },
      {
        id: 'income-statement',
        name: 'Income Statement',
        description: 'Revenue and expenses breakdown',
        type: 'financial',
        format: 'pdf'
      },
      {
        id: 'cash-flow',
        name: 'Cash Flow',
        description: 'Cash inflows and outflows analysis',
        type: 'financial',
        format: 'pdf'
      }
    ]
  },
  {
    id: 'expenses',
    name: 'Expense Reports',
    icon: 'account_balance_wallet',
    reports: [
      {
        id: 'expense-summary',
        name: 'Expense Summary',
        description: 'Categorized expense breakdown',
        type: 'financial',
        format: 'excel'
      },
      {
        id: 'maintenance-costs',
        name: 'Maintenance Costs',
        description: 'Property maintenance expenditure',
        type: 'operational',
        format: 'pdf'
      },
      {
        id: 'vendor-payments',
        name: 'Vendor Payments',
        description: 'Payments made to service providers',
        type: 'financial',
        format: 'excel'
      }
    ]
  },
  {
    id: 'tax',
    name: 'Tax Reports',
    icon: 'receipt_long',
    reports: [
      {
        id: 'tax-summary',
        name: 'Tax Summary',
        description: 'Annual tax reporting summary',
        type: 'tax',
        format: 'pdf'
      },
      {
        id: '1099',
        name: '1099 Forms',
        description: 'Vendor payment forms for tax purposes',
        type: 'tax',
        format: 'pdf'
      },
      {
        id: 'property-tax',
        name: 'Property Tax',
        description: 'Property tax payments and assessments',
        type: 'tax',
        format: 'excel'
      }
    ]
  },
  {
    id: 'property',
    name: 'Property Reports',
    icon: 'apartment',
    reports: [
      {
        id: 'occupancy',
        name: 'Occupancy Report',
        description: 'Property occupancy rates and trends',
        type: 'operational',
        format: 'pdf'
      },
      {
        id: 'lease-expiration',
        name: 'Lease Expiration',
        description: 'Upcoming lease renewals and expirations',
        type: 'operational',
        format: 'excel'
      },
      {
        id: 'property-performance',
        name: 'Property Performance',
        description: 'Key performance metrics by property',
        type: 'financial',
        format: 'pdf'
      }
    ]
  }
];