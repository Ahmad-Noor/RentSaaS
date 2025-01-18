import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeaseService } from '../../../services/lease.service';
import { LeaseTableComponent } from '../lease-table/lease-table.component';
import { SearchBarComponent } from '../../../../../../../shared/components/search-bar/search-bar.component';
import { Lease } from '../../../types/lease.types';

@Component({
  selector: 'app-lease-list',
  standalone: true,
  imports: [CommonModule, LeaseTableComponent, SearchBarComponent],
  template: `
    <div class="bg-white rounded-lg shadow">
      <div class="p-6">
        <div class="flex items-center justify-between mb-6">
          <app-search-bar 
            placeholder="Search lease agreements"
            (onSearch)="handleSearch($event)"
          />
          <div class="flex gap-2">
            <button class="p-2 text-gray-600 hover:bg-gray-100 rounded">
              <span class="material-icons">filter_list</span>
            </button>
            <button class="p-2 text-gray-600 hover:bg-gray-100 rounded">
              <span class="material-icons">download</span>
            </button>
          </div>
        </div>

        <app-lease-table
          [leases]="filteredLeases"
          (onAction)="handleAction($event)"
        />
      </div>
    </div>
  `
})
export class LeaseListComponent {
  leases: Lease[] = [];
  filteredLeases: Lease[] = [];

  constructor(private leaseService: LeaseService) {
    this.leaseService.getLeases().subscribe(leases => {
      this.leases = leases;
      this.filteredLeases = leases;
    });
  }

  handleSearch(term: string): void {
    this.filteredLeases = this.leases.filter(lease => 
      lease.tenantName.toLowerCase().includes(term.toLowerCase()) ||
      lease.propertyName.toLowerCase().includes(term.toLowerCase())
    );
  }

  handleAction(action: { type: string; lease: Lease }): void {
    switch (action.type) {
      case 'send':
        this.leaseService.updateLeaseStatus(action.lease.id, 'sent');
        break;
      case 'delete':
        this.leaseService.deleteLease(action.lease.id);
        break;
    }
  }
}