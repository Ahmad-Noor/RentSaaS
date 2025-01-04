import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-form-preview',
    imports: [CommonModule],
    template: `
    <div class="bg-white rounded-lg shadow-lg p-6">
      <h3 class="text-lg font-semibold mb-4">Lease Agreement Addendum</h3>
      
      <div class="space-y-6">
        <div>
          <h4 class="font-medium mb-2">Tenants</h4>
          <div class="flex gap-4">
            <div class="w-10 h-10 bg-blue-100 rounded-full flex items-center justify-center">
              JD
            </div>
            <div class="w-10 h-10 bg-yellow-100 rounded-full flex items-center justify-center">
              LW
            </div>
          </div>
        </div>

        <div class="space-y-4">
          <div>
            <label class="block text-sm font-medium text-gray-700">End Date</label>
            <div class="mt-1">
              <div class="flex items-center gap-2">
                <span>Will there be a change to the lease agreement end date?</span>
                <div class="flex gap-4">
                  <label class="flex items-center">
                    <input type="radio" checked class="text-blue-600" name="end-date">
                    <span class="ml-2">Yes</span>
                  </label>
                  <label class="flex items-center">
                    <input type="radio" class="text-blue-600" name="end-date">
                    <span class="ml-2">No</span>
                  </label>
                </div>
              </div>
            </div>
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700">Monthly Rent</label>
            <div class="mt-1">
              <div class="flex items-center gap-2">
                <span>Will there be a change to the monthly rent amount?</span>
                <div class="flex gap-4">
                  <label class="flex items-center">
                    <input type="radio" checked class="text-blue-600" name="rent">
                    <span class="ml-2">Yes</span>
                  </label>
                  <label class="flex items-center">
                    <input type="radio" class="text-blue-600" name="rent">
                    <span class="ml-2">No</span>
                  </label>
                </div>
              </div>
              <div class="mt-2">
                <input 
                  type="text" 
                  value="$2,500"
                  class="block w-32 rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                >
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class FormPreviewComponent {}