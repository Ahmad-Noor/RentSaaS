import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Property } from '../../types/property.types';
import { PropertyActionsComponent } from '../property-actions/property-actions.component';

@Component({
  selector: 'app-property-card',
  standalone: true,
  imports: [CommonModule, PropertyActionsComponent],
  template: `
    <div class="bg-white rounded-lg shadow overflow-hidden">
      <div class="relative h-48 bg-gray-200">
        <img 
          [src]="property.imageUrl || 'assets/placeholder-property.jpg'"
          [alt]="property.name"
          class="w-full h-full object-cover"
        >
        <button 
          class="absolute top-2 right-2 p-1 rounded-full bg-white/80 hover:bg-white"
          (click)="onFavorite.emit(property)"
        >
          <span class="material-icons text-gray-600">favorite_border</span>
        </button>
      </div>
      
      <div class="p-4">
        <div class="flex justify-between items-start mb-2">
          <h3 class="text-lg font-semibold">{{ property.name }}</h3>
          <span [class]="getStatusClass()">{{ property.status }}</span>
        </div>
        
        <div class="text-sm text-gray-600 mb-4">
          <p>{{ property.type }}</p>
          <p>{{ property.units }} Units | {{ property.occupancy }} Occupied</p>
        </div>
        
        <div class="flex justify-between items-center">
          <button 
            class="text-blue-600 hover:text-blue-700"
            (click)="onAction.emit({ type: 'edit', property: property })"
          >
            Edit
          </button>
          <app-property-actions 
            [property]="property"
            (onAction)="onAction.emit($event)"
          />
        </div>
      </div>
    </div>
  `
})
export class PropertyCardComponent {
  @Input() property!: Property;
  @Output() onAction = new EventEmitter<{ type: string; property: Property }>();
  @Output() onFavorite = new EventEmitter<Property>();

  getStatusClass(): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm';
    return this.property.status === 'Active'
      ? `${baseClasses} bg-green-100 text-green-800`
      : `${baseClasses} bg-gray-100 text-gray-800`;
  }
}