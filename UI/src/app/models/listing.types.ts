export interface ListingPhoto {
  id: string;
  file: File;
  name: string;
}

export interface ListingPlatforms {
  zillow: boolean;
  trulia: boolean;
  apartments: boolean;
  realtor: boolean;
}

export interface ListingFormData {
  propertyId: number;
  rent: number;
  deposit: number;
  description: string;
  availableFrom: string;
  leaseTerm: string;
  photos: ListingPhoto[];
  platforms: ListingPlatforms;
}