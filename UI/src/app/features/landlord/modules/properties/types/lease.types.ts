export type LeaseStatus = 'draft' | 'sent' | 'signed' | 'expired';
export type LeaseType = 'standard' | 'month-to-month' | 'sublease' | 'renewal';

export interface Lease {
  id: number;
  propertyId: number;
  propertyName: string;
  tenantName: string;
  type: LeaseType;
  startDate: string;
  endDate: string;
  monthlyRent: number;
  status: LeaseStatus;
  createdAt: string;
  updatedAt: string;
}

export interface CreateLeaseDTO {
  propertyId: number;
  tenantName: string;
  type: LeaseType;
  startDate: string;
  endDate: string;
  monthlyRent: number;
}