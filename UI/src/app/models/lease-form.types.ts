
export type LeaseStatus = 'draft' | 'sent' | 'signed' | 'expired';
export type LeaseType = 'standard' | 'month-to-month' | 'sublease' | 'renewal';

export interface LeaseFormDate{
    id: string;
    propertyId?: string;
    startDate?: string;
    endDate?: string;
    leaseType?: string;
    propertyName?: string;
    rentAmount?: number;
    tenantName?: string;
}