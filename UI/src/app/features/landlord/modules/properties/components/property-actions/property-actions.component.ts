import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Property } from '../../types/property.types';

@Component({
    selector: 'app-property-actions',
    imports: [CommonModule],
    template: `
    <div class="relative">
      <button 
        (click)="toggleMenu()"
        (blur)="closeMenu()"
        class="p-2 text-gray-600 hover:bg-gray-100 rounded-full"
      >
        <span class="material-icons">more_vert</span>
      </button>

      @if (isMenuOpen) {
        <div class="absolute right-0 mt-2 w-48 bg-white rounded-md shadow-lg z-50">
          <div class="py-1">
            <button
              (click)="onAction.emit({ type: 'edit', property })"
              class="w-full px-4 py-2 text-left text-sm text-gray-700 hover:bg-gray-100 flex items-center gap-2"
            >
              <span class="material-icons text-sm">edit</span>
              Edit Property
            </button>
            
            <button
              (click)="onAction.emit({ type: 'advertise', property })"
              class="w-full px-4 py-2 text-left text-sm text-gray-700 hover:bg-gray-100 flex items-center gap-2"
            >
              <span class="material-icons text-sm">campaign</span>
              Advertise
            </button>

            <button
              (click)="onAction.emit({ type: 'maintenance', property })"
              class="w-full px-4 py-2 text-left text-sm text-gray-700 hover:bg-gray-100 flex items-center gap-2"
            >
              <span class="material-icons text-sm">build</span>
              Maintenance
            </button>

            <button
              (click)="onAction.emit({ type: 'delete', property })"
              class="w-full px-4 py-2 text-left text-sm text-red-600 hover:bg-red-50 flex items-center gap-2"
            >
              <span class="material-icons text-sm">delete</span>
              Delete
            </button>
          </div>
        </div>
      }
    </div>
  `
})
export class PropertyActionsComponent {
  @Input() property!: Property;
  @Output() onAction = new EventEmitter<{ type: string; property: Property }>();
  
  isMenuOpen = false;

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
  }

  closeMenu(): void {
    setTimeout(() => {
      this.isMenuOpen = false;
    }, 200);
  }
}