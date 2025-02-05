export interface Report {
  id: string;
  name: string;
  description: string;
  type: 'financial' | 'operational' | 'tax';
  format: 'pdf' | 'excel' | 'csv';
}

export interface ReportCategory {
  id: string;
  name: string;
  icon: string;
  reports: Report[];
}