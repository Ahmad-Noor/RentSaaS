import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Application } from '../../../types/application.types';

@Component({
  selector: 'app-application-table',
  standalone: true,
  imports: [CommonModule],
  template: `
    <table class="w-full">
      <thead>
        <tr class="border-b">
          <th class="text-left py-3 px-4">Applicant</th>
          <th class="text-left py-3 px-4">Property</th>
          <th class="text-left py-3 px-4">Submitted</th>
          <th class="text-left py-3 px-4">Status</th>
          <th class="text-left py-3 px-4">Actions</th>
        </tr>
      </thead>
      <tbody>
        @for (application of applications; track application.id) {
          <tr class="border-b hover:bg-gray-50">
            <td class="py-3 px-4">
              <div>{{ application.applicantName }}</div>
              <div class="text-sm text-gray-500">{{ application.email }}</div>
            </td>
            <td class="py-3 px-4">{{ application.propertyName }}</td>
            <td class="py-3 px-4">
              {{ application.submittedAt | date:'mediumDate' }}
            </td>
            <td class="py-3 px-4">
              <span [class]="getStatusClass(application.status)">
                {{ application.status }}
              </span>
            </td>
            <td class="py-3 px-4">
              <div class="flex gap-2">
                @if (application.status === 'new' || application.status === 'reviewing') {
                  <button 
                    class="p-1 text-green-600 hover:bg-green-50 rounded"
                    (click)="onAction.emit({ type: 'approve', application })"
                  >
                    <span class="material-icons">check_circle</span>
                  </button>
                  <button 
                    class="p-1 text-red-600 hover:bg-red-50 rounded"
                    (click)="onAction.emit({ type: 'reject', application })"
                  >
                    <span class="material-icons">cancel</span>
                  </button>
                }
                <button 
                  class="p-1 text-gray-600 hover:bg-gray-100 rounded"
                  (click)="onAction.emit({ type: 'delete', application })"
                >
                  <span class="material-icons">delete</span>
                </button>
              </div>
            </td>
          </tr>
        }
      </tbody>
    </table>
  `
})
export class ApplicationTableComponent {
  @Input() applications: Application[] = [];
  @Output() onAction = new EventEmitter<{ type: string; application: Application }>();

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm capitalize';
    const statusClasses: Record<string, string> = {
      'new': 'bg-blue-100 text-blue-800',
      'reviewing': 'bg-yellow-100 text-yellow-800',
      'approved': 'bg-green-100 text-green-800',
      'rejected': 'bg-red-100 text-red-800'
    };

    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }
}