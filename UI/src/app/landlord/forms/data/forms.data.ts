import { FormCategory } from '../types/forms.types';

export const FORM_CATEGORIES: FormCategory[] = [
  {
    id: 'lease',
    name: 'Lease Agreements',
    icon: 'description',
    forms: [
      { id: 'lease-agreement', name: 'Standard Lease Agreement' },
      { id: 'lease-addendum', name: 'Lease Addendum' },
      { id: 'lease-renewal', name: 'Lease Renewal Agreement' },
      { id: 'sublease', name: 'Sublease Agreement' },
      { id: 'roommate', name: 'Roommate Agreement' }
    ]
  },
  {
    id: 'notices',
    name: 'Tenant Notices',
    icon: 'notifications',
    forms: [
      { id: 'rent-increase', name: 'Rent Increase Notice' },
      { id: 'late-payment', name: 'Late Payment Notice' },
      { id: 'lease-violation', name: 'Lease Violation Notice' },
      { id: 'entry-notice', name: 'Notice of Entry' },
      { id: 'non-renewal', name: 'Non-Renewal Notice' }
    ]
  },
  {
    id: 'move',
    name: 'Move In/Out',
    icon: 'home',
    forms: [
      { id: 'move-in-checklist', name: 'Move-In Checklist' },
      { id: 'move-out-checklist', name: 'Move-Out Checklist' },
      { id: 'condition-report', name: 'Property Condition Report' },
      { id: 'security-deposit', name: 'Security Deposit Receipt' },
      { id: 'key-receipt', name: 'Key Receipt Form' }
    ]
  },
  {
    id: 'maintenance',
    name: 'Maintenance',
    icon: 'build',
    forms: [
      { id: 'maintenance-request', name: 'Maintenance Request Form' },
      { id: 'repair-authorization', name: 'Repair Authorization' },
      { id: 'inspection-report', name: 'Property Inspection Report' },
      { id: 'service-log', name: 'Maintenance Service Log' }
    ]
  },
  {
    id: 'financial',
    name: 'Financial Forms',
    icon: 'payments',
    forms: [
      { id: 'rent-receipt', name: 'Rent Receipt' },
      { id: 'payment-plan', name: 'Payment Plan Agreement' },
      { id: 'late-fee', name: 'Late Fee Agreement' },
      { id: 'expense-report', name: 'Property Expense Report' }
    ]
  },
  {
    id: 'legal',
    name: 'Legal Notices',
    icon: 'gavel',
    forms: [
      { id: 'eviction-notice', name: 'Notice to Quit' },
      { id: 'legal-summons', name: 'Legal Summons Template' },
      { id: 'court-filing', name: 'Court Filing Checklist' },
      { id: 'settlement', name: 'Settlement Agreement' }
    ]
  }
];