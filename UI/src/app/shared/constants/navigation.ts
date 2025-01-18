export interface NavItem {
  icon: string;
  label: string;
  route: string;
  children?: NavItem[];
}

export interface NavGroup {
  label: string;
  items: NavItem[];
}

export const LANDLORD_NAVIGATION: NavGroup[] = [
  {
    label: 'Property Management',
    items: [
      { 
        icon: 'business', 
        label: 'Companies', 
        route: '/landlord/companies'
      },
      { 
        icon: 'apartment', 
        label: 'Properties', 
        route: '/landlord/properties',
        children: [
          { icon: 'campaign', label: 'Advertising', route: '/landlord/properties/advertising' },
          { icon: 'description', label: 'Applications & Leads', route: '/landlord/properties/applications' },
          { icon: 'gavel', label: 'Lease Agreement', route: '/landlord/properties/lease' }
        ]
      }
    ]
  },
  {
    label: 'Financial',
    items: [
      {
        icon: 'payments',
        label: 'Payments',
        route: '/landlord/financial/payments',
        children: [
          { icon: 'attach_money', label: 'Record Payment', route: '/landlord/financial/payments/record' },
          { icon: 'history', label: 'Payment History', route: '/landlord/financial/payments/history' }
        ]
      },
      { icon: 'account_balance_wallet', label: 'Expenses', route: '/landlord/financial/expenses' },
      { icon: 'calculate', label: 'Taxes', route: '/landlord/financial/taxes' },
      { icon: 'assessment', label: 'Reports', route: '/landlord/financial/reports' }
    ]
  },
  {
    label: 'Services',
    items: [
      { 
        icon: 'build', 
        label: 'Maintenance', 
        route: '/landlord/maintenance',
        children: [
          { icon: 'list_alt', label: 'Requests List', route: '/landlord/maintenance/requests' }
        ]
      },
      { icon: 'mail', label: 'Mailbox', route: '/landlord/messages' },
      { icon: 'description', label: 'Forms & Notices', route: '/landlord/forms' }
    ]
  },
  {
    label: 'Administration',
    items: [
      { icon: 'groups', label: 'Team Management', route: '/landlord/team' },
      { icon: 'manage_accounts', label: 'Users', route: '/landlord/users' },
      { icon: 'build', label: 'Website Studio', route: '/landlord/websitestudio' },
      { icon: 'build', label: 'AI Agent for real estate opportunity ', route: '/landlord/websitestudio'  },
      { icon: 'build', label: 'AI Real Estate Voice Agent(https://www.youtube.com/watch?v=Miaob3BQpdk)', route: '/landlord/websitestudio'  },
      { icon: 'build', label: 'Project Management', route: '/landlord/projectmanagement' }
    ]
  }
];