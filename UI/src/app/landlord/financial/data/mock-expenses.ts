import { Expense } from '../../../models/expense.types';

export const MOCK_EXPENSES: Expense[] = [
  {
    id: 1,
    date: '2024-01-15',
    description: 'Monthly Insurance Premium',
    amount: 450.00,
    category: 'insurance',
    status: 'paid',
    recurring: true
  },
  {
    id: 2,
    date: '2024-01-20',
    description: 'Property Tax Payment',
    amount: 2500.00,
    category: 'taxes',
    status: 'pending',
    dueDate: '2024-02-01'
  },
  {
    id: 3,
    date: '2024-01-22',
    description: 'Emergency Plumbing Repair',
    amount: 350.00,
    category: 'maintenance',
    status: 'paid',
    propertyId: 1
  },
  {
    id: 4,
    date: '2024-01-25',
    description: 'Utility Bills - January',
    amount: 780.00,
    category: 'utilities',
    status: 'overdue',
    dueDate: '2024-01-20'
  },
  {
    id: 5,
    date: '2024-02-01',
    description: 'Mortgage Payment',
    amount: 3200.00,
    category: 'mortgage',
    status: 'pending',
    recurring: true,
    dueDate: '2024-02-05'
  }
];