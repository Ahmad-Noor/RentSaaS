import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { SearchBarComponent } from '../../../../shared/components/search-bar/search-bar.component';
import { Lease } from '../../../../models/lease.types';
import { LeaseService } from '../../../../service/lease.service';

@Component({
  selector: 'app-lease-page',
  standalone: true,
  imports: [CommonModule, SearchBarComponent],
  templateUrl: './lease.page.html'
})
export class LeasePage {
  leases: Lease[] = [];
  filteredLeases: Lease[] = [];

  constructor(private leaseService: LeaseService, private router: Router) {
    this.fetchLeases();
  }

  fetchLeases(): void {
    this.leaseService.getLeases().subscribe(leases => {
      this.leases = leases;
      this.filteredLeases = leases;
    });
  }

  handleSearch(term: string): void {
    if (!term.trim()) {
      this.filteredLeases = this.leases;
      return;
    }

    const lowerCaseTerm = term.toLowerCase();
    this.filteredLeases = this.leases.filter(lease => 
      (lease.tenantName?.toLowerCase().includes(lowerCaseTerm) || false) ||
      (lease.propertyName?.toLowerCase().includes(lowerCaseTerm) || false)
    );
  }

  async handleAction(action: { type: string; lease: Lease }): Promise<void> {
    switch (action.type) {
      case "view":
        this.router.navigate(['/leases', action.lease.id]);
        break;
      
      case "edit":
        this.router.navigate(['/leases/edit', action.lease.id]);
        break;

      case "delete":
        this.deleteLease(action.lease);
        break;
    }
  }

  deleteLease(lease: Lease): any {
    if (confirm("Are you sure you want to delete this lease?")) {
      this.leaseService.deleteLease(lease.id).subscribe(() => {
        this.leases = this.leases.filter(l => l.id !== lease.id);
        this.filteredLeases = this.filteredLeases.filter(l => l.id !== lease.id);
      });
    }
  }

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm capitalize';
    const statusClasses: Record<string, string> = {
      draft: 'bg-gray-100 text-gray-800',
      sent: 'bg-blue-100 text-blue-800',
      signed: 'bg-green-100 text-green-800',
      expired: 'bg-red-100 text-red-800'
    };
    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }

  trackByLeaseId(index: number, lease: Lease): string {
    return lease.id;
  }
}
