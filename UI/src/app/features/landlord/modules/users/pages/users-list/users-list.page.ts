import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { DataTableComponent, TableColumn } from '../../../../../../shared/components/data-table/data-table.component';
import { ActionBarComponent } from '../../../../../../shared/components/action-bar/action-bar.component';

@Component({
  selector: 'app-users-list-page',
  standalone: true,
  imports: [CommonModule, RouterLink, DataTableComponent, ActionBarComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <h1 class="text-2xl font-semibold">Users</h1>
        <a 
          routerLink="add"
          class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors"
        >
          <span class="material-icons text-sm">add</span>
          Add User
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-action-bar searchPlaceholder="Search users" />
          <app-data-table 
            [columns]="columns"
            [data]="users"
          />
        </div>
      </div>
    </div>
  `
})
export class UsersListPage {
  columns: TableColumn[] = [
    { key: 'name', label: 'Name' },
    { key: 'email', label: 'Email' },
    { key: 'role', label: 'Role' },
    { key: 'status', label: 'Status', type: 'status' },
    { key: 'actions', label: 'Actions', type: 'actions' }
  ];

  users = [
    {
      id: 1,
      name: 'John Doe',
      email: 'john@example.com',
      role: 'Administrator',
      status: 'Active'
    }
  ];
}