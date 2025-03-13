import { FileWithMetadata } from "./fileWithMetadata.types";
export type LeaseTerm = 'month-to-month' | '6 months' | '1 year' | '2 years';

export interface  AdvertisingFormData{
 id?: string;
   propertyId?: number;
   monthlyRent: number;
   securityDeposit: number;
   details?: string;
   availablefrom?: Date;
   leaseTerm: LeaseTerm;
   files?: FileWithMetadata[];
     zillow: boolean;
     trulia: boolean;
     apartments: boolean;
     realtor: boolean;
}