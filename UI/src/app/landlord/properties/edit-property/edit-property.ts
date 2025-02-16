import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Property } from '../../../models/property.model';
import { PropertyActionsComponent } from '../property-actions/property-actions.component';

@Component({
  selector: 'app-property-card',
  standalone: true,
  imports: [CommonModule, PropertyActionsComponent],
  templateUrl: './edit-property.html',
  styleUrls: ['./edit-property.css']
})
export class Editproperty {
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