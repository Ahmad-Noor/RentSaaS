import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaintenanceService } from '../../services/maintenance.service';
import { MaintenanceRequest } from '../../types/maintenance.types';

@Component({
  selector: 'app-request-list',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="bg-white rounded-lg shadow">
      <div class="p-6">
        <div class="flex items-center justify-between mb-4">
          <div class="relative">
            <span class="material-icons absolute left-3 top-2 text-gray-400">search</span>
            <input 
              type="text"
              placeholder="Search requests"
              class="pl-10 pr-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            >
          </div>
          <div class="flex gap-2">
            <button class="p-2 text-gray-600 hover:bg-gray-100 rounded">
              <span class="material-icons">filter_list</span>
            </button>
            <button class="p-2 text-gray-600 hover:bg-gray-100 rounded">
              <span class="material-icons">download</span>
            </button>
          </div>
        </div>

        <table class="w-full">
          <thead>
            <tr class="border-b">
              <th class="text-left py-3 px-4">ID</th>
              <th class="text-left py-3 px-4">Property</th>
              <th class="text-left py-3 px-4">Issue</th>
              <th class="text-left py-3 px-4">Status</th>
              <th class="text-left py-3 px-4">Priority</th>
              <th class="text-left py-3 px-4">Actions</th>
            </tr>
          </thead>
          <tbody>
            @for (request of requests; track request.id) {
              <tr class="border-b">
                <td class="py-3 px-4">#{{ request.id }}</td>
                <td class="py-3 px-4">{{ request.propertyId }}</td>
                <td class="py-3 px-4">{{ request.issueType }}</td>
                <td class="py-3 px-4">
                  <span [class]="getStatusClass(request.status)">
                    {{ request.status }}
                  </span>
                </td>
                <td class="py-3 px-4">
                  <span [class]="getPriorityClass(request.priority)">
                    {{ request.priority }}
                  </span>
                </td>
                <td class="py-3 px-4">
                  <button class="text-gray-600 hover:text-gray-900">
                    <span class="material-icons">more_vert</span>
                  </button>
                </td>
              </tr>
            }
          </tbody>
        </table>
      </div>
    </div>
  `
})
export class RequestListComponent {
  requests: MaintenanceRequest[] = [];

  constructor(private maintenanceService: MaintenanceService) {
    this.maintenanceService.getRequests().subscribe(requests => {
      this.requests = requests;
    });
  }

  getStatusClass(status?: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm';
    const statusClasses: Record<string, string> = {
      'pending': 'bg-yellow-100 text-yellow-800',
      'in_progress': 'bg-blue-100 text-blue-800',
      'completed': 'bg-green-100 text-green-800',
      'cancelled': 'bg-gray-100 text-gray-800'
    };

    return `${baseClasses} ${statusClasses[status || 'pending']}`;
  }

  getPriorityClass(priority?: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm';
    const priorityClasses: Record<string, string> = {
      'low': 'bg-gray-100 text-gray-800',
      'medium': 'bg-yellow-100 text-yellow-800',
      'high': 'bg-orange-100 text-orange-800',
      'emergency': 'bg-red-100 text-red-800'
    };

    return `${baseClasses} ${priorityClasses[priority || 'low']}`;
  }
}