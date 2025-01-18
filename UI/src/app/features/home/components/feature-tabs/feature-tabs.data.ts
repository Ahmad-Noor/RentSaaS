import { FeatureTab } from './feature-tab.interface';

export const FEATURE_TABS: FeatureTab[] = [
  {
    id: 'messaging',
    label: 'Messaging',
    content: {
      title: 'Your professional messaging app',
      description: 'Keep your phone number private from leads and applicants, and keep tenant communication in one place.',
      imageUrl: '/assets/images/messaging-feature.png'
    }
  },
  {
    id: 'maintenance',
    label: 'Maintenance',
    content: {
      title: 'Built-in, professional maintenance requests',
      description: 'Tenants can easily submit any issues from their portal, and you can keep a paper trail of all maintenance performed.',
      imageUrl: '/assets/images/maintenance-feature.png'
    }
  },
  {
    id: 'expenses',
    label: 'Expenses',
    content: {
      title: 'Say goodbye to shoebox receipts',
      description: 'Track and store your expenses with ease. Attach receipts, then filter by rental unit, category, and time. Then, export as a CSV.',
      imageUrl: '/assets/images/expenses-feature.png'
    }
  },
  {
    id: 'e-sign',
    label: 'E-Sign',
    content: {
      title: 'Sign important docs from anywhere',
      description: 'Collect e-signatures online. Send reminders with one click, and get alerts when the documents you send are signed.',
      imageUrl: '/assets/images/esign-feature.png'
    }
  },
  {
    id: 'condition-reports',
    label: 'Condition Reports',
    content: {
      title: 'Customize. Send. Sign. Store.',
      description: 'Protect yourself from any "he said, she said" conflicts and security deposit disputes by using our condition reports.',
      imageUrl: '/assets/images/condition-reports-feature.png'
    }
  },
  {
    id: 'forms',
    label: 'Forms',
    content: {
      title: 'All the forms you need to succeed',
      description: 'Access 32 essential rental forms, from welcome letters to rent increase notices. Available for download in PDF format.',
      imageUrl: '/assets/images/forms-feature.png'
    }
  }
];