import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-condition-reports',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <section class="bg-[#F8FBFF] py-16">
      <div class="max-w-7xl mx-auto px-4">
        <div class="flex items-center gap-12">
          <div class="flex-1">
            <h2 class="text-4xl font-bold mb-6">Customize. Send.<br>Sign. Store.</h2>
            <p class="text-lg text-gray-600 mb-4">
              Protect yourself from any "he said, she said" conflicts and security deposit disputes by
              using our condition reports.
            </p>
            <a 
              routerLink="/pricing"
              class="text-blue-600 hover:text-blue-700 font-medium inline-flex items-center"
            >
              Learn more
              <span class="material-icons ml-1">arrow_forward</span>
            </a>
          </div>
          <div class="flex-1">
            <div class="bg-white rounded-lg shadow-lg p-6">
              <div class="flex items-center justify-between mb-4">
                <h3 class="font-semibold">Bedroom #1</h3>
                <button class="text-gray-400">&times;</button>
              </div>
              <div class="flex gap-4 mb-6">
                <div class="text-center">
                  <div class="w-10 h-10 rounded-full bg-red-100 flex items-center justify-center mb-1">
                    <span class="text-red-600 material-icons">close</span>
                  </div>
                  <span class="text-sm">Poor</span>
                </div>
                <div class="text-center">
                  <div class="w-10 h-10 rounded-full bg-gray-100 flex items-center justify-center mb-1">
                    <span class="text-gray-600 material-icons">remove</span>
                  </div>
                  <span class="text-sm">Fair</span>
                </div>
                <div class="text-center">
                  <div class="w-10 h-10 rounded-full bg-green-100 flex items-center justify-center mb-1">
                    <span class="text-green-600 material-icons">check</span>
                  </div>
                  <span class="text-sm">Good</span>
                </div>
              </div>
              <div class="space-y-4">
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-1">Note (Optional)</label>
                  <textarea 
                    class="w-full p-2 border rounded-md"
                    placeholder="Door handle is busted and not functioning properly."
                  ></textarea>
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-1">Attachments (Optional)</label>
                  <button class="flex items-center gap-2 text-sm text-gray-600 hover:text-gray-800">
                    <span class="material-icons">photo_camera</span>
                    Photo or Video
                  </button>
                </div>
                <div class="flex items-center gap-2">
                  <input type="checkbox" id="maintenance" class="rounded border-gray-300">
                  <label for="maintenance" class="text-sm text-gray-700">Create maintenance request</label>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  `
})
export class ConditionReportsComponent {}