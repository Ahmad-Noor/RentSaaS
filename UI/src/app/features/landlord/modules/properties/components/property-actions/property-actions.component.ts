import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Property } from '../../types/property.types';

@Component({
  selector: 'app-property-actions',
  standalone: true,
  imports: [CommonModule],
  templateUrl:'./property-actions.component.html',
  styleUrl:'./property-actions.component.css',
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