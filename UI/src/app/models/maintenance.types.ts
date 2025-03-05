import { FileWithMetadata } from "./fileWithMetadata.types";
export type IssueType = 'plumbing' | 'electrical' | 'hvac' | 'appliance' | 'structural' | 'other';
export type MaintenancePriority = 'low' | 'medium' | 'high' | 'emergency';
export type MaintenanceStatus = 'pending' | 'in_progress' | 'completed' | 'cancelled';

// export interface Photo {
//   id: string;
//   file: File;
//   name: string;
//   size: number;
//   type: string;
// }

export interface MaintenanceRequest {
  id: string;
  propertyId: number;
  property: string;
  issueType: IssueType;
  priority: MaintenancePriority;
  details: string;
  files?: FileWithMetadata[];
  status: MaintenanceStatus;
  dueDate: string;
  createdAt?: string;
  updatedAt?: string;
}