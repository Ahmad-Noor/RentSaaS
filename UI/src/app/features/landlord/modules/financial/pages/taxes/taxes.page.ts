import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaxSummaryComponent } from '../../components/taxes/tax-summary/tax-summary.component';
import { TaxPaymentsComponent } from '../../components/taxes/tax-payments/tax-payments.component';
import { TaxDocumentsComponent } from '../../components/taxes/tax-documents/tax-documents.component';

@Component({
  selector: 'app-taxes-page',
  standalone: true,
  imports: [CommonModule, TaxSummaryComponent, TaxPaymentsComponent, TaxDocumentsComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Tax Management</h1>
          <p class="mt-1 text-gray-600">Track and manage property-related taxes</p>
        </div>
        <button class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors">
          <span class="material-icons text-sm">download</span>
          Export Tax Documents
        </button>
      </div>

      <app-tax-summary />
      <app-tax-payments />
      <app-tax-documents />
    </div>
  `
})
export class TaxesPage {}