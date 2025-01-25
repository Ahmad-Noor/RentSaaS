import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { DataTableComponent, TableColumn } from '../../../../../../shared/components/data-table/data-table.component';
import { ActionBarComponent } from '../../../../../../shared/components/action-bar/action-bar.component';

@Component({
  selector: 'app-advertising-page',
  standalone: true,
  imports: [CommonModule, RouterLink, DataTableComponent, ActionBarComponent],
    templateUrl: `./advertising.page.html`
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