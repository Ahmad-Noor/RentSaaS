import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-button',
    imports: [CommonModule],
    templateUrl: './button.component.html',
    standalone: true,
    styleUrls: ['./button.component.css']
    



    
})
export class ButtonComponent {
  @Input() type: 'button' | 'submit' = 'button';
  @Input() variant: 'primary' | 'secondary' | 'outline' = 'primary';
  @Input() disabled = false;
  @Input() loading = false;

  getButtonClasses(): string {
    const baseClasses = 'w-full rounded-md py-2 px-4 transition-colors disabled:opacity-50 bg-orange-500';
    
    switch (this.variant) {
      case 'primary':
        return `${baseClasses} bg-orange-600 text-white hover:bg-blue-700 disabled:hover:bg-orange-600`;
      case 'secondary':
        return `${baseClasses} bg-white border border-gray-300 hover:bg-gray-50 bg-orange-500`;
      default:
        return baseClasses;
    }
  }
}