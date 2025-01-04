import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tab-button',
  standalone: true,
  imports: [CommonModule],
  template: `
    <button 
      (click)="onClick.emit()"
      class="px-6 py-2 rounded-full text-sm font-medium transition-colors"
      [class.bg-blue-600]="isActive"
      [class.text-white]="isActive"
      [class.text-gray-600]="!isActive"
      [class.hover:bg-blue-50]="!isActive"
    >
      {{ label }}
    </button>
  `
})
export class TabButtonComponent {
  @Input() label = '';
  @Input() isActive = false;
  @Output() onClick = new EventEmitter<void>();
}