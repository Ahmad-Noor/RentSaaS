import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-expense-type-management',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="relative">
      <button
        type="button"
        (click)="showForm = true"
        class="text-sm text-blue-600 hover:text-blue-700 flex items-center gap-1"
      >
        <span class="material-icons text-sm">add</span>
        Add New Type
      </button>

      @if (showForm) {
        <div class="absolute top-full right-0 mt-2 p-4 bg-white rounded-lg shadow-lg border w-64 z-10">
          <form [formGroup]="typeForm" (ngSubmit)="onSubmit()" class="space-y-4">
            <div>
              <label for="name" class="block text-sm font-medium text-gray-700">Type Name</label>
              <input
                type="text"
                id="name"
                formControlName="name"
                class="mt-1 w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                placeholder="Enter type name"
              >
            </div>

            <div class="flex justify-end gap-2">
              <button
                type="button"
                (click)="showForm = false"
                class="px-3 py-1 text-sm text-gray-600 hover:text-gray-700"
              >
                Cancel
              </button>
              <button
                type="submit"
                [disabled]="!typeForm.valid"
                class="px-3 py-1 text-sm bg-blue-600 text-white rounded hover:bg-blue-700 disabled:opacity-50"
              >
                Add Type
              </button>
            </div>
          </form>
        </div>
      }
    </div>
  `
})
export class ExpenseTypeManagementComponent {
  @Output() onAddType = new EventEmitter<string>();
  
  showForm = false;
  typeForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.typeForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]]
    });
  }

  onSubmit(): void {
    if (this.typeForm.valid) {
      this.onAddType.emit(this.typeForm.value.name);
      this.typeForm.reset();
      this.showForm = false;
    }
  }
}