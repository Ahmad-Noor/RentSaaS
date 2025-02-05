import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormListComponent } from './components/form-list.component';
import { FormPreviewComponent } from './components/form-preview.component';

@Component({
    selector: 'app-forms-section',
    imports: [CommonModule, FormListComponent, FormPreviewComponent],
    template: `
    <section class="bg-[#F8FBFF] py-16">
      <div class="max-w-7xl mx-auto px-4">
        <div class="flex items-center gap-12">
          <div class="flex-1">
            <h2 class="text-4xl font-bold mb-6">All the forms you need to succeed</h2>
            <p class="text-lg text-gray-600 mb-8">
              Access 32 essential rental forms, from welcome letters to rent increase notices.
              Available for download in PDF format.
            </p>
            <app-form-list />
          </div>
          <div class="flex-1">
            <app-form-preview />
          </div>
        </div>
      </div>
    </section>
  `
})
export class FormsSectionComponent {}