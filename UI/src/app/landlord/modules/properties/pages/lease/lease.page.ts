import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { LeaseService } from '../../services/lease.service';
import { Lease } from '../../types/lease.types';
import { SearchBarComponent } from '../../../../../shared/components/search-bar/search-bar.component';
 
@Component({
  selector: 'app-lease-page',
  standalone: true,
  imports: [CommonModule, RouterLink, SearchBarComponent],
  templateUrl: `./lease.page.html`
})
//export class LeasePage {}
export class LeasePage {
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

  @Output() onAction = new EventEmitter<{ type: string; lease: Lease }>();

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm capitalize';
    const statusClasses: Record<string, string> = {
      'draft': 'bg-gray-100 text-gray-800',
      'sent': 'bg-blue-100 text-blue-800',
      'signed': 'bg-green-100 text-green-800',
      'expired': 'bg-red-100 text-red-800'
    };

    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }

  trackByLeaseId(index: number, lease: Lease): number {
    return lease.id;
  }
}
