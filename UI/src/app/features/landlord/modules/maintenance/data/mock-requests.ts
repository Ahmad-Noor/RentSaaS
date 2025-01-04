import { MaintenanceRequest } from '../types/maintenance.types';

export const MOCK_REQUESTS: MaintenanceRequest[] = [
  {
    id: 1,
    propertyId: 1,
    issueType: 'plumbing',
    priority: 'high',
    description: 'Water leak under kitchen sink. Water is pooling on the floor and needs immediate attention.',
    status: 'in_progress',
    createdAt: '2024-01-15T08:30:00Z',
    updatedAt: '2024-01-15T10:15:00Z'
  },
  {
    id: 2,
    propertyId: 2,
    issueType: 'electrical',
    priority: 'medium',
    description: 'Multiple power outlets in living room not working. No visible damage to outlets.',
    status: 'pending',
    createdAt: '2024-01-14T15:20:00Z',
    updatedAt: '2024-01-14T15:20:00Z'
  },
  {
    id: 3,
    propertyId: 1,
    issueType: 'hvac',
    priority: 'high',
    description: 'AC not cooling properly. Temperature remains high despite unit running constantly.',
    status: 'completed',
    createdAt: '2024-01-10T09:00:00Z',
    updatedAt: '2024-01-12T14:30:00Z'
  },
  {
    id: 4,
    propertyId: 3,
    issueType: 'appliance',
    priority: 'low',
    description: 'Dishwasher making unusual noise during wash cycle. Still functioning but concerning.',
    status: 'pending',
    createdAt: '2024-01-13T11:45:00Z',
    updatedAt: '2024-01-13T11:45:00Z'
  },
  {
    id: 5,
    propertyId: 2,
    issueType: 'structural',
    priority: 'emergency',
    description: 'Large crack appeared in living room ceiling after recent heavy rain. Possible roof leak.',
    status: 'in_progress',
    createdAt: '2024-01-15T07:15:00Z',
    updatedAt: '2024-01-15T08:00:00Z'
  }
];