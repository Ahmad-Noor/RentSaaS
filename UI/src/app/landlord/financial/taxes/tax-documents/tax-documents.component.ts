import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaxDocument } from '../../../../models/tax.types';

@Component({
  selector: 'app-tax-documents',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="bg-white rounded-lg shadow-sm">
      <div class="p-4 border-b">
        <h2 class="text-lg font-semibold">Tax Documents</h2>
      </div>
      <div class="p-4">
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          @for (document of documents; track document.id) {
            <div class="border rounded-lg p-4">
              <div class="flex items-start justify-between">
                <div class="flex items-center gap-3">
                  <span class="material-icons text-gray-400">
                    {{ document.type === 'pdf' ? 'picture_as_pdf' : 'description' }}
                  </span>
                  <div>
                    <div class="font-medium">{{ document.name }}</div>
                    <div class="text-sm text-gray-500">{{ document.year }}</div>
                  </div>
                </div>
                <button class="p-2 text-gray-600 hover:text-blue-600 rounded-lg hover:bg-blue-50">
                  <span class="material-icons">download</span>
                </button>
              </div>
            </div>
          }
        </div>
      </div>
    </div>
  `
})
export class TaxDocumentsComponent {
  documents: TaxDocument[] = [
    {
      id: 1,
      name: '1099-MISC Forms',
      year: '2023',
      type: 'pdf',
      category: 'income'
    },
    {
      id: 2,
      name: 'Property Tax Assessments',
      year: '2023',
      type: 'pdf',
      category: 'property'
    },
    {
      id: 3,
      name: 'Tax Deductions Summary',
      year: '2023',
      type: 'excel',
      category: 'deductions'
    },
    {
      id: 4,
      name: 'Quarterly Tax Payments',
      year: '2023',
      type: 'pdf',
      category: 'payments'
    },
    {
      id: 5,
      name: 'Property Depreciation Schedule',
      year: '2023',
      type: 'excel',
      category: 'depreciation'
    },
    {
      id: 6,
      name: 'Tax Return Documents',
      year: '2023',
      type: 'pdf',
      category: 'returns'
    }
  ];
}