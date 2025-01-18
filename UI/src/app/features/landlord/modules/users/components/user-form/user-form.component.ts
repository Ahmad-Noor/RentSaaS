import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormFieldComponent } from '../../../../../../shared/components/form-field/form-field.component';
import { CreateUserDTO } from '../../types/user.types';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent],
  template: `
    <form [formGroup]="userForm" (ngSubmit)="handleSubmit()" class="space-y-6">
      <div class="grid grid-cols-2 gap-4">
        <app-form-field label="First Name" id="firstName">
          <input
            type="text"
            id="firstName"
            formControlName="firstName"
            class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
          >
        </app-form-field>

        <app-form-field label="Last Name" id="lastName">
          <input
            type="text"
            id="lastName"
            formControlName="lastName"
            class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
          >
        </app-form-field>
      </div>

      <app-form-field label="Email" id="email">
        <input
          type="email"
          id="email"
          formControlName="email"
          class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
        >
      </app-form-field>

      <app-form-field label="Role" id="role">
        <select
          id="role"
          formControlName="role"
          class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
        >
          <option value="">Select role</option>
          <option value="admin">Administrator</option>
          <option value="manager">Property Manager</option>
          <option value="staff">Staff Member</option>
          <option value="readonly">Read Only</option>
        </select>
      </app-form-field>

      <div class="space-y-4">
        <h3 class="text-lg font-medium">Permissions</h3>
        
        <div class="space-y-2">
          @for (permission of permissions; track permission.value) {
            <label class="flex items-center gap-2">
              <input
                type="checkbox"
                [formControlName]="permission.value"
                class="rounded border-gray-300 text-blue-600"
              >
              <span>{{ permission.label }}</span>
            </label>
          }
        </div>
      </div>

      <div class="flex justify-end gap-4">
        <button
          type="submit"
          [disabled]="!userForm.valid || loading"
          class="px-4 py-2 text-white bg-blue-600 rounded hover:bg-blue-700 disabled:opacity-50"
        >
          {{ loading ? 'Creating...' : 'Create User' }}
        </button>
      </div>
    </form>
  `
})
export class UserFormComponent {
  @Output() submit = new EventEmitter<CreateUserDTO>();

  userForm: FormGroup;
  loading = false;

  permissions = [
    { label: 'View properties', value: 'viewProperties' },
    { label: 'Manage properties', value: 'manageProperties' },
    { label: 'View financial data', value: 'viewFinancials' },
    { label: 'Manage financial data', value: 'manageFinancials' },
    { label: 'View maintenance', value: 'viewMaintenance' },
    { label: 'Manage maintenance', value: 'manageMaintenance' }
  ];

  constructor(private fb: FormBuilder) {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      role: ['', Validators.required],
      viewProperties: [false],
      manageProperties: [false],
      viewFinancials: [false],
      manageFinancials: [false],
      viewMaintenance: [false],
      manageMaintenance: [false]
    });

    // Update permissions based on role selection
    this.userForm.get('role')?.valueChanges.subscribe(role => {
      this.updatePermissionsByRole(role);
    });
  }

  private updatePermissionsByRole(role: string): void {
    const permissions = {
      admin: {
        viewProperties: true,
        manageProperties: true,
        viewFinancials: true,
        manageFinancials: true,
        viewMaintenance: true,
        manageMaintenance: true
      },
      manager: {
        viewProperties: true,
        manageProperties: true,
        viewFinancials: true,
        manageFinancials: false,
        viewMaintenance: true,
        manageMaintenance: true
      },
      staff: {
        viewProperties: true,
        manageProperties: false,
        viewFinancials: false,
        manageFinancials: false,
        viewMaintenance: true,
        manageMaintenance: false
      },
      readonly: {
        viewProperties: true,
        manageProperties: false,
        viewFinancials: false,
        manageFinancials: false,
        viewMaintenance: true,
        manageMaintenance: false
      }
    };

    if (role in permissions) {
      Object.entries(permissions[role as keyof typeof permissions]).forEach(([key, value]) => {
        this.userForm.get(key)?.setValue(value);
      });
    }
  }

  handleSubmit(): void {
    if (this.userForm.valid) {
      this.loading = true;
      this.submit.emit(this.userForm.value);
    }
  }
}