import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Form } from '../../../models/forms.types';

@Component({
  selector: 'app-form-item',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="flex items-center justify-between py-2">
      <div class="flex items-center gap-3">
        <span class="material-icons text-gray-400">description</span>
        <span>{{ form.name }}</span>
      </div>
      <div class="flex items-center gap-2">
        <button 
          class="p-2 text-gray-600 hover:text-blue-600 rounded-lg hover:bg-blue-50"
          (click)="previewForm()"
        >
          <span class="material-icons">visibility</span>
        </button>
        <button 
          class="p-2 text-gray-600 hover:text-blue-600 rounded-lg hover:bg-blue-50"
          (click)="downloadForm()"
        >
          <span class="material-icons">download</span>
        </button>
      </div>
    </div>
  `
})
export class FormItemComponent {
  @Input() form!: Form;

  previewForm(): void {
    // TODO: Implement form preview
    console.log('Preview form:', this.form);
  }

  downloadForm(): void {
    // TODO: Implement form download
    console.log('Download form:', this.form);
  }
}