export type LeaseStatus = 'draft' | 'sent' | 'signed' | 'expired';
export type LeaseType = 'standard' | 'month-to-month' | 'sublease' | 'renewal';

 
export interface Lease
{

    id?: any;
    propertyId?: string;
    startDate?: Date;
    endDate?: Date;
    rentAmount?: number;
    tenantName?: string;
    leaseType?: string;
    propertyName?: string;  
    // status??:any;
}
 