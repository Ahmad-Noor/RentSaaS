export type UserRole = 'admin' | 'manager' | 'staff' | 'readonly';
export type UserStatus = 'active' | 'inactive' | 'pending';

export interface User {
  id?: string;
  organizationId?: string;
  firstName: string;
  lastName: string;
  email: string;
  role: UserRole;
  status: UserStatus;
  permissions: UserPermissions;
  createdAt: string;
  updatedAt: string;
}

export interface UserPermissions {
  viewProperties: boolean;
  manageProperties: boolean;
  viewFinancial: boolean;
  manageFinancial: boolean;
  viewMaintenance: boolean;
  manageMaintenance: boolean;
}

export interface CreateUserDTO {
  firstName: string;
  lastName: string;
  email: string;
  role: UserRole;
  permissions: UserPermissions;
}