import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTableComponent, TableColumn } from '../../../../../../shared/components/data-table/data-table.component';
import { ActionBarComponent } from '../../../../../../shared/components/action-bar/action-bar.component';

@Component({
  selector: 'app-team-list-page',
  standalone: true,
  imports: [CommonModule, DataTableComponent, ActionBarComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <h1 class="text-2xl font-semibold">Team Management</h1>
        <button class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors">
          <span class="material-icons text-sm">add</span>
          Add Team Member
        </button>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-action-bar searchPlaceholder="Search team members" />
          <app-data-table 
            [columns]="columns"
            [data]="teamMembers"
          />
        </div>
      </div>
    </div>
  `
})
export class TeamListPage {
  columns: TableColumn[] = [
    { key: 'name', label: 'Name' },
    { key: 'role', label: 'Role' },
    { key: 'department', label: 'Department' },
    { key: 'status', label: 'Status', type: 'status' },
    { key: 'actions', label: 'Actions', type: 'actions' }
  ];

  teamMembers = [
    {
      id: 1,
      name: 'John Smith',
      role: 'Property Manager',
      department: 'Operations',
      status: 'Active'
    },
    {
      id: 2,
      name: 'Sarah Johnson',
      role: 'Maintenance Supervisor',
      department: 'Maintenance',
      status: 'Active'
    }
  ];
}