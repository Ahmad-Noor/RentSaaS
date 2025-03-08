import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {  RouterModule } from '@angular/router';
import { RouterLink, Router, ActivatedRoute } from "@angular/router";
import { ConfirmDialogService } from "../../../../shared/services/confirm-dialog/confirm-dialog.service";
import { SearchBarComponent } from '../../../../shared/components/search-bar/search-bar.component';
import { Lease } from '../../../../models/lease.types';
import { LeaseService } from '../../../../service/lease.service';

@Component({
  selector: 'app-lease-page',
  standalone: true,
  imports: [CommonModule, SearchBarComponent, RouterModule,RouterLink],
  templateUrl: './lease.page.html'
})
export class LeasePage implements OnInit{
  leases: Lease[] = [];
  filteredLeases: Lease[] = [];

  constructor(private leaseService: LeaseService, private router: Router ,  private route: ActivatedRoute,  private confirmDialog: ConfirmDialogService) {
    // this.fetchLeases();
  }

  ngOnInit(): void {
    this.loadLeases();
    
  }

  loadLeases() {
    this.leaseService.getLeases().subscribe({
      next: (leases) => {
        console.log('Fetched Leases:', leases); // تحقق من البيانات
        this.leases = this.processLeases(leases);
        this.filteredLeases = [...this.leases];
      },
      error: (error) => {
        console.error('Failed to load leases:', error);
      }
    });
  }
  processLeases(leases: any[]): Lease[] {
    return leases.map(lease => {
      // Derive status from isPaid and dueDate
      
      return {
        ...lease,
      };
    });
  }

 


  // fetchLeases(): void {
  //   this.leaseService.getLeases().subscribe(leases => {
  //     console.log('Fetched leases:', leases); // Debugging line
  //     this.leases = leases.map(lease => ({
  //       ...lease,
  //       propertyName: lease.propertyName || 'No Property Name' // Ensure propertyName is set
  //     }));
  //     this.filteredLeases = [...this.leases];
  //   }, error => {
  //     console.error('Error fetching leases:', error);
  //   });
  // }

  handleSearch(term: string): void {
    if (!term.trim()) {
      this.filteredLeases = this.leases;
      return;
    }
    const lowerCaseTerm = term.toLowerCase();
    this.filteredLeases = this.leases.filter(lease => 
      (lease.propertyName?.toLowerCase().includes(lowerCaseTerm) || false) ||
      (lease.tenantName?.toLowerCase().includes(lowerCaseTerm) || false)
    );
  }


  deleteLease(lease: Lease): void {
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
    return `${baseClasses} ${statusClasses[status] || "bg-gray-100 text-gray-800"}`;
  }

  trackByLeaseId(index: number, lease: Lease): string {
    return lease.id;
  }


  handleEditAction(lease: Lease) {
    this.router.navigate(['lease-add-edit', lease.id], {
      relativeTo: this.route,
      state: { lease }
    });
  }
  
  
  async handleDeleteAction(lease: Lease) {
    const isConfirmed = await this.confirmDialog.show({
      title: "Delete lease",
      message: "Are you sure you want to delete this lease?",
      confirmText: "Delete",
      cancelText: "Cancel",
      type: "danger",
    });
    
    if (isConfirmed) {
      this.leaseService.deleteLease(lease.id).subscribe({
        next: () => {
          // Remove expense from both arrays to update UI
          this.leases = this.leases.filter(e => e.id !== lease.id);
          this.filteredLeases = this.filteredLeases.filter(e => e.id !== lease.id);
        },
        error: (error) => {
          console.error('Error deleting lease:', error);
        }
      });
    }
  }
}
