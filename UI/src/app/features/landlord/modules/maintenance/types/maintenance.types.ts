export type IssueType = 'plumbing' | 'electrical' | 'hvac' | 'appliance' | 'structural' | 'other';
export type Priority = 'low' | 'medium' | 'high' | 'emergency';
export type Status = 'pending' | 'in_progress' | 'completed' | 'cancelled';

export interface Photo {
  id: string;
  file: File;
  name: string;
  size: number;
  type: string;
}

export interface MaintenanceRequest {
  id?: number;
  propertyId: number;
  issueType: IssueType;
  priority: Priority;
  description: string;
  photos?: Photo[];
  status?: Status;
  createdAt?: string;
  updatedAt?: string;
}