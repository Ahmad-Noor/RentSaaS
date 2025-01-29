import { Component, EventEmitter, Input, Output, TrackByFunction } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';


interface TeamMember {
  id: number;
  name: string;
  role: string;
  department: string;
  status: string;
  [key: string]: any;
}
interface TableColumn {
  key: keyof TeamMember | 'actions';
  label: string;
  type?: 'status' | 'actions';
}

@Component({
  selector: 'app-team-list-page',
  standalone: true,
  imports: [CommonModule],
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
    <input 
        type="text" 
        class="pl-10 pr-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
        placeholder="Search users" 
        (input)="handleSearch($event)"
      />
      
      <table class="w-full">
        <thead>
          <tr class="border-b">
            <th *ngFor="let column of columns" class="text-left py-3 px-4">{{ column.label }}</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let row of filteredTeamMembers; trackBy: trackByFn" class="border-b hover:bg-gray-50">
            <td *ngFor="let column of columns" class="py-3 px-4">
              <ng-container [ngSwitch]="column.type">
                <span *ngSwitchCase="'status'" [class]="getStatusClass(row[column.key])">
                {{ row[column.key] }}
                </span>
                <div *ngSwitchCase="'actions'" class="flex gap-2">
                  <button 
                    class="text-gray-600 hover:text-blue-600"
                    (click)="onAction.emit({ type: 'edit', item: row })"
                  >
                    <span class="material-icons">edit</span>
                  </button>
                  <button 
                      class="text-gray-600 hover:text-red-600"
                      (click)="handleDelete(row.id)"
                    >
                      <span class="material-icons">delete</span>
                    </button>
                </div>
                <span *ngSwitchDefault>
                    {{ row[column.key] }}
                  </span>
              </ng-container>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</div>

  `
})
 
export class TeamListPage {
  
 
  @Output() onAction = new EventEmitter<{ type: string; item: any }>();

  columns = [
    { key: 'name', label: 'Name' },
    { key: 'role', label: 'Role' },
    { key: 'department', label: 'Department' },
    { key: 'status', label: 'Status', type: 'status' },
    { key: 'actions', label: 'Actions', type: 'actions' }
  ];

  teamMembers: TeamMember[] = [
    { id: 1, name: 'John Smith', role: 'Property Manager', department: 'Operations', status: 'Active' },
    { id: 2, name: 'Sarah Johnson', role: 'Maintenance Supervisor', department: 'Maintenance', status: 'Active' }
  ];

  filterText = '';  
  filteredTeamMembers: TeamMember[] = this.teamMembers;  

  constructor(private router: Router) {}

  trackByFn(index: number, item: any): number {
    return item.id;
  }


  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm';
    const statusClasses: Record<string, string> = {
      'Active': 'bg-green-100 text-green-800',
      'Inactive': 'bg-gray-100 text-gray-800',
      'Pending': 'bg-yellow-100 text-yellow-800',
      'Completed': 'bg-blue-100 text-blue-800'
    };

    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }

  handleAction(type: string, item: any): void {
    this.onAction.emit({ type, item });
  }

  handleSearch(event: Event): void {
    const inputElement = event.target as HTMLInputElement; // Cast to HTMLInputElement
    const term = inputElement.value;
    this.filteredTeamMembers = this.teamMembers.filter(user =>
      user.name.toLowerCase().includes(term.toLowerCase()) ||
      user.role.toLowerCase().includes(term.toLowerCase())
    );
  }

  handleDelete(userId: number): void {
    // Filter out the deleted user from both filteredUsers and users
    this.teamMembers = this.teamMembers.filter(user => user.id !== userId);
    this.filteredTeamMembers = this.filteredTeamMembers.filter(user => user.id !== userId);
  }

  handleEdit(userId: number): void {
    // Navigate to the edit page with the userId as a route parameter
    this.router.navigate([`/edit/${userId}`]);
  }

  trackById(index: number, row: TeamMember): number {
    return row.id;
  }
    // Fixing this method to properly emit action
    onRowAction(type: string, item: TeamMember) {
      this.onAction.emit({ type, item });
    }
}