import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { DataTableComponent, TableColumn } from '../../../../../../shared/components/data-table/data-table.component';
import { ActionBarComponent } from '../../../../../../shared/components/action-bar/action-bar.component';

@Component({
  selector: 'app-advertising-page',
  standalone: true,
  imports: [CommonModule, RouterLink, DataTableComponent, ActionBarComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <h1 class="text-2xl font-semibold">Property Advertising</h1>
        <a 
          routerLink="create"
          class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors"
        >
          <span class="material-icons text-sm">add</span>
          Create Listing
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-action-bar searchPlaceholder="Search listings" />
          <app-data-table 
            [columns]="columns"
            [data]="listings"
          />
        </div>
      </div>
    </div>
  `
})
export class AdvertisingPage {
  columns: TableColumn[] = [
    { key: 'property', label: 'Property' },
    { key: 'platform', label: 'Platform' },
    { key: 'views', label: 'Views' },
    { key: 'leads', label: 'Leads' },
    { key: 'status', label: 'Status', type: 'status' },
    { key: 'actions', label: 'Actions', type: 'actions' }
  ];

  listings = [
    {
      id: 1,
      property: 'Sunset Apartments',
      platform: 'Zillow',
      views: '1,245',
      leads: '23',
      status: 'Active'
    },
    {
      id: 2,
      property: 'Downtown Lofts',
      platform: 'Apartments.com',
      views: '892',
      leads: '15',
      status: 'Active'
    }
  ];
}