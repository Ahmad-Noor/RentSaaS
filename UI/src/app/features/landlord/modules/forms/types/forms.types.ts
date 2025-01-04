export interface Form {
  id: string;
  name: string;
  description?: string;
  templateUrl?: string;
}

export interface FormCategory {
  id: string;
  name: string;
  icon: string;
  forms: Form[];
}