import { UUID } from "crypto";
export type PropertyListView = "list" | "grid";
export type PropertyType = "house" | "condo" | "townhouse" | "community";
export type PropertyStatus = "Active" | "Inactive" | "Pending";

export interface PropertyCreate {
  note: string;
  address: string;
  unite: null;
}

export interface Property {
  id?: UUID;
  address: string;
  createdBy: string;
}
