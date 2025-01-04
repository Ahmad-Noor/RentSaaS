import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-form-field',
    imports: [CommonModule],
    template: `
    <div>
      <label [for]="id" class="block text-sm font-medium text-gray-700">{{ label }}</label>
      <ng-content></ng-content>
      <p *ngIf="error" class="mt-1 text-sm text-red-600">{{ error }}</p>
    </div>
  `
})
export class FormFieldComponent {
  @Input() label!: string;
  @Input() id!: string;
  @Input() error?: string;
}