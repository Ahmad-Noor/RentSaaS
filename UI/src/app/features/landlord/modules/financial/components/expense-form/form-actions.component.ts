import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-form-actions',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="flex justify-end gap-4">
      <a
        routerLink="../"
        class="px-4 py-2 text-gray-700 hover:text-gray-900"
      >
        Cancel
      </a>
      <button
        type="submit"
        [disabled]="disabled || loading"
        class="px-4 py-2 text-white bg-blue-600 rounded hover:bg-blue-700 disabled:opacity-50"
      >
        {{ loading ? 'Saving...' : 'Save Expense' }}
      </button>
    </div>
  `
})
export class FormActionsComponent {
  @Input() loading = false;
  @Input() disabled = false;
}