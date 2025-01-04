export interface RentalForm {
  id: string;
  name: string;
  category: 'lease' | 'notice' | 'inspection' | 'maintenance';
}

export const RENTAL_FORMS: RentalForm[] = [
  {
    id: 'state-lease',
    name: 'State-Specific Lease Agreement',
    category: 'lease'
  },
  {
    id: 'lease-addendum',
    name: 'Lease Agreement Addendum',
    category: 'lease'
  },
  {
    id: 'cosigner',
    name: 'Co-Signer Agreement',
    category: 'lease'
  },
  {
    id: 'move-in',
    name: 'Move In/Out Condition Report',
    category: 'inspection'
  },
  {
    id: 'pay-notice',
    name: 'Notice to Pay Rent or Quit',
    category: 'notice'
  },
  {
    id: 'entry-notice',
    name: '24 Hour Notice to Enter',
    category: 'notice'
  },
  {
    id: 'inspection',
    name: 'Property Inspection',
    category: 'inspection'
  },
  {
    id: 'maintenance',
    name: 'Preventative Maintenance Schedule',
    category: 'maintenance'
  }
];