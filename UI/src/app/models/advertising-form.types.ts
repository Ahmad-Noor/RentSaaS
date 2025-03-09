import { FileWithMetadata } from "./fileWithMetadata.types";
export type LeaseTerm = 'month-to-month' | '6 months' | '1 year' | '2 years';
export type PublishingPlatform = 'zillow' | 'trulia' | 'apartments.com' | 'realtor.com';

export interface  AdvertisingFormData{
  id?: string;
  property: string;
  monthlyRent: number;
  securityDeposit: number;
  description: string;
  availableFrom: Date;
  leaseTerm: LeaseTerm;
  propertyPhotos?: FileWithMetadata[];
  publishingPlatforms: {
    zillow: boolean;
    trulia: boolean;
    apartmentsCom: boolean;
    realtorCom: boolean;
  };
  createdAt?: string;
  updatedAt?: string;
}