export type ApplicationStatus = 'new' | 'reviewing' | 'approved' | 'rejected';

export interface Application {
  id: string;
  propertyId: string;
  applicantEmail: string;
  phoneNumber: number;
  message?: string;
  requestbackgroundcheck?: boolean;
  requestcreditreport?: boolean;
}

export interface ApplicationFormData {
  propertyId: string;
  applicantEmail: string;
  phoneNumber: number;
  message?: string;
  requestbackgroundcheck?: boolean;
  requestcreditreport?: boolean;
}
