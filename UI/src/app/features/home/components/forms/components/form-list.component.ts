import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RENTAL_FORMS } from '../data/forms.data';

@Component({
    selector: 'app-form-list',
    imports: [CommonModule],
    template: `
    <div class="bg-white rounded-lg shadow-lg p-6">
      <h3 class="text-lg font-semibold mb-4">Forms</h3>
      <div class="space-y-3">
        @for (form of forms; track form.id) {
          <div class="flex items-center justify-between">
            <div class="flex items-center gap-3">
              <span class="material-icons text-gray-400">description</span>
              <span>{{ form.name }}</span>
            </div>
            <button class="text-blue-600 hover:text-blue-700 flex items-center gap-1">
              <span class="material-icons text-sm">download</span>
              Download
            </button>
          </div>
        }
      </div>
    </div>
  `
})
export class FormListComponent {
  forms = RENTAL_FORMS;
}