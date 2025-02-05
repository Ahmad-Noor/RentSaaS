import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Property } from '../../types/property.types';
import { PropertyActionsComponent } from '../property-actions/property-actions.component';

@Component({
  selector: 'app-property-card',
  standalone: true,
  imports: [CommonModule, PropertyActionsComponent],
  templateUrl: './property-card.component.html',
  styleUrls: ['./property-card.component.css']
})
export class PropertyCardComponent {
  @Input() property!: Property;
  @Output() onAction = new EventEmitter<{ type: string; property: Property }>();
  @Output() onFavorite = new EventEmitter<Property>();

  getStatusClass(): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm';
    return "";
    // return this.property.isDeleted === false
    //   ? `${baseClasses} bg-green-100 text-green-800`
    //   : `${baseClasses} bg-gray-100 text-gray-800`;
  }
}