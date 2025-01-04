export type PropertyListView = 'list' | 'grid';

export type PropertyType = 'house' | 'condo' | 'townhouse' | 'community';
export type PropertyStatus = 'Active' | 'Inactive' | 'Pending';

export interface Property {
  id: number;
  name: string;
  address: string;
  propertyType: PropertyType;
  type: string;
  units: string;
  occupancy: string;
  status: PropertyStatus;
  unitNumber?: string;
  imageUrl?: string;
}

export interface CreatePropertyDTO {
  address: string;
  propertyType: PropertyType;
  unitNumber?: string;
}