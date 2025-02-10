export type ApplicationStatus = 'new' | 'reviewing' | 'approved' | 'rejected';

export interface Application {
  id: number;
  propertyId: number;
  propertyName: string;
  applicantName: string;
  email: string;
  phone: string;
  status: ApplicationStatus;
  submittedAt: string;
  desiredMoveIn?: string;
  creditScore?: number;
  income?: number;
}



export interface ApplicationCreate {
  organizationId: string;
  createdAt: string; 
  createdBy: string;
  propertyId: string;
  applicantEmail: string;
  phoneNumber: number;
  message: string;
  requestBackgroundCheck: boolean;
  requestCreditReport: boolean;
}

 
