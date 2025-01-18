export interface Company {
  id: number;
  name: string;
  type: string;
  properties: string;
  employees: string;
  status: 'Active' | 'Inactive';
}