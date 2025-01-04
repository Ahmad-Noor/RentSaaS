import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyService } from '../../services/company.service';

@Component({
    selector: 'app-company-form-page',
    imports: [CommonModule, RouterLink, ReactiveFormsModule],
    template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <h1 class="text-2xl font-semibold">{{ isEditMode ? 'Edit' : 'Add New' }} Company</h1>
        <a 
          routerLink=".."
          class="text-gray-600 px-4 py-2 rounded flex items-center gap-2 hover:bg-gray-100 transition-colors"
        >
          <span class="material-icons text-sm">arrow_back</span>
          Back to List
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <form [formGroup]="companyForm" (ngSubmit)="onSubmit()" class="p-6 space-y-6">
          <div class="grid grid-cols-2 gap-6">
            <!-- Company Name -->
            <div class="space-y-2">
              <label for="name" class="block text-sm font-medium text-gray-700">Company Name</label>
              <input
                type="text"
                id="name"
                formControlName="name"
                class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
              >
            </div>

            <!-- Company Type -->
            <div class="space-y-2">
              <label for="type" class="block text-sm font-medium text-gray-700">Company Type</label>
              <select
                id="type"
                formControlName="type"
                class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
              >
                <option value="">Select type</option>
                <option value="Property Management">Property Management</option>
                <option value="Real Estate">Real Estate</option>
                <option value="Investment">Investment</option>
              </select>
            </div>

            <!-- Number of Properties -->
            <div class="space-y-2">
              <label for="properties" class="block text-sm font-medium text-gray-700">Number of Properties</label>
              <input
                type="number"
                id="properties"
                formControlName="properties"
                class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
              >
            </div>

            <!-- Number of Employees -->
            <div class="space-y-2">
              <label for="employees" class="block text-sm font-medium text-gray-700">Number of Employees</label>
              <input
                type="number"
                id="employees"
                formControlName="employees"
                class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
              >
            </div>
          </div>

          <!-- Description -->
          <div class="space-y-2">
            <label for="description" class="block text-sm font-medium text-gray-700">Description</label>
            <textarea
              id="description"
              formControlName="description"
              rows="4"
              class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
            ></textarea>
          </div>

          <!-- Form Actions -->
          <div class="flex justify-end gap-4">
            <button
              type="button"
              routerLink=".."
              class="px-4 py-2 border rounded hover:bg-gray-50"
            >
              Cancel
            </button>
            <button
              type="submit"
              [disabled]="!companyForm.valid || loading"
              class="px-4 py-2 bg-[#0078D4] text-white rounded hover:bg-[#106EBE] disabled:opacity-50"
            >
              {{ loading ? 'Saving...' : 'Save Company' }}
            </button>
          </div>
        </form>
      </div>
    </div>
  `
})
export class CompanyFormPage implements OnInit {
  companyForm: FormGroup;
  isEditMode = false;
  loading = false;
  companyId?: number;

  constructor(
    private fb: FormBuilder,
    private companyService: CompanyService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.companyForm = this.fb.group({
      name: ['', [Validators.required]],
      type: ['', [Validators.required]],
      properties: ['', [Validators.required]],
      employees: ['', [Validators.required]],
      description: [''],
      status: ['Active']
    });
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.companyId = parseInt(id, 10);
      this.loadCompany(this.companyId);
    }
  }

  private loadCompany(id: number) {
    this.loading = true;
    this.companyService.getCompany(id).subscribe({
      next: (company) => {
        if (company) {
          this.companyForm.patchValue(company);
        }
      },
      error: (error) => {
        console.error('Error loading company:', error);
      },
      complete: () => {
        this.loading = false;
      }
    });
  }

  onSubmit() {
    if (this.companyForm.valid) {
      this.loading = true;
      const data = this.companyForm.value;

      const request = this.isEditMode && this.companyId
        ? this.companyService.updateCompany(this.companyId, data)
        : this.companyService.createCompany(data);

      request.subscribe({
        next: () => {
          this.router.navigate(['..'], { relativeTo: this.route });
        },
        error: (error) => {
          console.error('Error saving company:', error);
          this.loading = false;
        }
      });
    }
  }
}