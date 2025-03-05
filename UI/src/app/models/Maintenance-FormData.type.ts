
export interface MaintenanceFormData {
    propertyId: number;
    property: string;
    issueType: 'Plumbing' | 'Electrical' | 'HVAC' | 'Appliance' | 'Structural' | 'Other';
    priority: 'Low' | 'Medium' | 'High' | 'Emergency';
    description: string;
    photos?: File[];
    dueDate: string;
    status?: boolean;
}