import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormItemComponent } from '../form-item/form-item.component';
import { FormCategory } from '../../../models/forms.types';

@Component({
  selector: 'app-form-category',
  standalone: true,
  imports: [CommonModule, FormItemComponent],
  template: `
    <div class="bg-white rounded-lg shadow-sm">
      <div class="p-4 border-b">
        <div class="flex items-center gap-3">
          <span class="material-icons text-blue-600">{{ category.icon }}</span>
          <h2 class="text-lg font-semibold">{{ category.name }}</h2>
        </div>
      </div>
      
      <div class="p-4">
        <div class="space-y-3">
          @for (form of category.forms; track form.id) {
            <app-form-item [form]="form" />
          }
        </div>
      </div>
    </div>
  `
})
export class FormCategoryComponent {
  @Input() category!: FormCategory;
}