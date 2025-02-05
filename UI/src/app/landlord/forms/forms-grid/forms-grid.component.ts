import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormCategoryComponent } from '../form-category/form-category.component';
import { FORM_CATEGORIES } from '../data/forms.data';

@Component({
  selector: 'app-forms-grid',
  standalone: true,
  imports: [CommonModule, FormCategoryComponent],
  template: `
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
      @for (category of categories; track category.id) {
        <app-form-category [category]="category" />
      }
    </div>
  `
})
export class FormsGridComponent {
  categories = FORM_CATEGORIES;
}