import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-listing-platforms',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div [formGroup]="formGroup">
      <label class="block text-sm font-medium text-gray-700 mb-4">
        Publishing Platforms
      </label>
      
      <div formGroupName="platforms" class="grid grid-cols-2 md:grid-cols-4 gap-4">
        <label class="relative flex items-center justify-center p-4 border rounded-lg cursor-pointer hover:bg-gray-50">
          <input
            type="checkbox"
            formControlName="zillow"
            class="absolute top-2 right-2"
          >
          <div class="text-center">
            <span class="material-icons text-blue-600 text-2xl mb-2">home</span>
            <div>Zillow</div>
          </div>
        </label>

        <label class="relative flex items-center justify-center p-4 border rounded-lg cursor-pointer hover:bg-gray-50">
          <input
            type="checkbox"
            formControlName="trulia"
            class="absolute top-2 right-2"
          >
          <div class="text-center">
            <span class="material-icons text-green-600 text-2xl mb-2">apartment</span>
            <div>Trulia</div>
          </div>
        </label>

        <label class="relative flex items-center justify-center p-4 border rounded-lg cursor-pointer hover:bg-gray-50">
          <input
            type="checkbox"
            formControlName="apartments"
            class="absolute top-2 right-2"
          >
          <div class="text-center">
            <span class="material-icons text-purple-600 text-2xl mb-2">location_city</span>
            <div>Apartments.com</div>
          </div>
        </label>

        <label class="relative flex items-center justify-center p-4 border rounded-lg cursor-pointer hover:bg-gray-50">
          <input
            type="checkbox"
            formControlName="realtor"
            class="absolute top-2 right-2"
          >
          <div class="text-center">
            <span class="material-icons text-red-600 text-2xl mb-2">business</span>
            <div>Realtor.com</div>
          </div>
        </label>
      </div>
    </div>
  `
})
export class ListingPlatformsComponent {
  @Input() formGroup!: FormGroup;
}