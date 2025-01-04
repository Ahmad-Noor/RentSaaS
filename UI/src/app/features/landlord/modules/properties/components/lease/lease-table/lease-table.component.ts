import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Lease } from '../../../types/lease.types';

@Component({
  selector: 'app-lease-table',
  standalone: true,
  imports: [CommonModule],
  template: `
    <table class="w-full">
      <thead>
        <tr class="border-b">
          <th class="text-left py-3 px-4">Property</th>
          <th class="text-left py-3 px-4">Tenant</th>
          <th class="text-left py-3 px-4">Type</th>
          <th class="text-left py-3 px-4">Start Date</th>
          <th class="text-right py-3 px-4">Monthly Rent</th>
          <th class="text-left py-3 px-4">Status</th>
          <th class="text-left py-3 px-4">Actions</th>
        </tr>
      </thead>
      <tbody>
        @for (lease of leases; track lease.id) {
          <tr class="border-b hover:bg-gray-50">
            <td class="py-3 px-4">{{ lease.propertyName }}</td>
            <td class="py-3 px-4">{{ lease.tenantName }}</td>
            <td class="py-3 px-4 capitalize">{{ lease.type }}</td>
            <td class="py-3 px-4">{{ lease.startDate | date:'mediumDate' }}</td>
            <td class="py-3 px-4 text-right">{{ lease.monthlyRent | currency }}</td>
            <td class="py-3 px-4">
              <span [class]="getStatusClass(lease.status)">
                {{ lease.status }}
              </span>
            </td>
            <td class="py-3 px-4">
              <div class="flex gap-2">
                @if (lease.status === 'draft') {
                  <button 
                    class="p-1 text-blue-600 hover:bg-blue-50 rounded"
                    (click)="onAction.emit({ type: 'send', lease })"
                  >
                    <span class="material-icons">send</span>
                  </button>
                }
                <button 
                  class="p-1 text-gray-600 hover:bg-gray-100 rounded"
                  (click)="onAction.emit({ type: 'view', lease })"
                >
                  <span class="material-icons">visibility</span>
                </button>
                <button 
                  class="p-1 text-gray-600 hover:bg-gray-100 rounded"
                  (click)="onAction.emit({ type: 'delete', lease })"
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
export class LeaseTableComponent {
  @Input() leases: Lease[] = [];
  @Output() onAction = new EventEmitter<{ type: string; lease: Lease }>();

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm capitalize';
    const statusClasses: Record<string, string> = {
      'draft': 'bg-gray-100 text-gray-800',
      'sent': 'bg-blue-100 text-blue-800',
      'signed': 'bg-green-100 text-green-800',
      'expired': 'bg-red-100 text-red-800'
    };

    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }
}