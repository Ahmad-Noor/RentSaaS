import { Component, EventEmitter, Input, OnChanges, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ActionBarComponent } from '../../../../../../shared/components/action-bar/action-bar.component';

interface Listing {
  id: number;
  property: string;
  platform: string;
  views: string;
  leads: string;
  status: string;
}

interface TableColumn {
  key: keyof Listing;  // This ensures column.key is always a valid property of Listing
  label: string;
  type?: 'status' | 'default';
}

@Component({
  selector: 'app-advertising-page',
  standalone: true,
  imports: [CommonModule, ActionBarComponent],
  templateUrl: './advertising.page.html',
})
export class AdvertisingPage implements OnInit, OnChanges {
  @Input() data!: Listing[];   
  @Output() onAction = new EventEmitter<{ type: string; item: Listing }>();
  
  columns: (TableColumn | { key: 'actions', label: string })[] = [
    { key: 'id', label: 'ID' },
    { key: 'status', label: 'Status', type: 'status' },
    { key: 'property', label: 'Property' },
    { key: 'platform', label: 'Platform' },
    { key: 'views', label: 'Views' },
    { key: 'leads', label: 'Leads' },
    { key: 'actions', label: 'Actions' }
  ];
  
  listings: Listing[] = [
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

  constructor() {}

  ngOnInit() {}

  ngOnChanges() {}

  handleAction(action: { type: string; item: Listing }) {
    // Action handler for edit/delete or any other actions
    console.log(action);
  }

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm';
    const statusClasses: Record<string, string> = {
      'Active': 'bg-green-100 text-green-800',
      'Inactive': 'bg-gray-100 text-gray-800',
      'Pending': 'bg-yellow-100 text-yellow-800',
    };
    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }

  handleSearch(query: string) {
    console.log('Search query:', query);
    // Add your logic to filter or search listings
  }

  toStr(value: any): string {
    return String(value);
  }
}
