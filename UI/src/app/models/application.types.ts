export type ApplicationStatus = 'new' | 'reviewing' | 'approved' | 'rejected';

export interface Application {
    id: string;
    propertyId: string;
    applicantEmail: string;
    phoneNumber: number;
    message?: string;
    requestBackground?: boolean;
    requestCredit?: boolean;
}

export interface ApplicationCreate {
  propertyId: string;
  applicantEmail: string;
  phoneNumber: number;
  message?: string;
  requestBackground?: boolean;
  requestCredit?: boolean;
}
