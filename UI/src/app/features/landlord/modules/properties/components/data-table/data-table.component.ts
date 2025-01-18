import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Property } from '../../types/property.types';
import { PropertyActionsComponent } from '../property-actions/property-actions.component';

@Component({
  selector: 'app-property-table',
  standalone: true,
  imports: [CommonModule, PropertyActionsComponent],
  template: `
    <table class="w-full">
      <thead>
        <tr class="border-b">
          <th class="text-left py-3 px-4">Property Name</th>
          <th class="text-left py-3 px-4">Type</th>
          <th class="text-left py-3 px-4">Units</th>
          <th class="text-left py-3 px-4">Occupancy</th>
          <th class="text-left py-3 px-4">Status</th>
          <th class="text-left py-3 px-4">Actions</th>
        </tr>
      </thead>
      <tbody>
        @for (property of properties; track property.id) {
          <tr class="border-b hover:bg-gray-50">
            <td class="py-3 px-4">{{ property.name }}</td>
            <td class="py-3 px-4">{{ property.type }}</td>
            <td class="py-3 px-4">{{ property.units }}</td>
            <td class="py-3 px-4">{{ property.occupancy }}</td>
            <td class="py-3 px-4">
              <span [class]="getStatusClass(property.status)">
                {{ property.status }}
              </span>
            </td>
            <td class="py-3 px-4">
              <app-property-actions 
                [property]="property"
                (onAction)="onAction.emit($event)"
              />
            </td>
          </tr>
        }
      </tbody>
    </table>
  `
})
export class PropertyTableComponent {
  @Input() properties: Property[] = [];
  @Output() onAction = new EventEmitter<{ type: string; property: Property }>();

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm';
    const statusClasses: Record<string, string> = {
      'Active': 'bg-green-100 text-green-800',
      'Inactive': 'bg-gray-100 text-gray-800',
      'Pending': 'bg-yellow-100 text-yellow-800'
    };

    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }
}