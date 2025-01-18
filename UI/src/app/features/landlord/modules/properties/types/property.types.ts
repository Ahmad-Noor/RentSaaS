export type PropertyListView = 'list' | 'grid';

export type PropertyType = 'house' | 'condo' | 'townhouse' | 'community';
export type PropertyStatus = 'Active' | 'Inactive' | 'Pending';






export interface PropertyCreate {
  note:           string;
  address:        string;
  unite:          null;
}



export interface Property {




  address:        string;
  unite:          null;
  id:             string;
  organizationId: string;
  note:           string;
  isDeleted:      boolean;
  leases:         null;
  createdAt:      Date;
  createdBy:      string;
  lastModifiedAt: Date;
  lastModifiedBy: string;
  deletedAt:      Date;
  deletedBy:      string;
  type:      string;


  // id: number;
  // name: string;
  // address: string;
  // propertyType: PropertyType;
  // type: string;
  // units: string;
  // occupancy: string;
  // status: PropertyStatus;
  // unitNumber?: string;
  // imageUrl?: string;
}



export interface CreatePropertyDTO {
  address: string;
  propertyType: PropertyType;
  unitNumber?: string;
}