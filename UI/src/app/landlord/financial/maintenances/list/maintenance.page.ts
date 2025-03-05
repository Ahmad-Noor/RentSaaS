import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterLink, Router, ActivatedRoute } from "@angular/router";
import { MaintenanceRequest } from "../../../../models/maintenance.types";
import { MaintenanceService } from "../../../../service/maintenance.service";
import { ConfirmDialogService } from "../../../../shared/services/confirm-dialog/confirm-dialog.service";

@Component({
  selector: "app-maintenances-page",
  standalone: true,
  imports: [CommonModule],
  templateUrl: "./maintenances.page.html",
})
export class MaintenancesPage implements OnInit {
  maintenance: MaintenanceRequest[] = [];
  filteredmaintenances: MaintenanceRequest[] = [];
  @Output() onAction = new EventEmitter<{ type: string; maintenance: MaintenanceRequest }>();

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private maintenanceService: MaintenanceService,
    private confirmDialog: ConfirmDialogService
  ) { }

  ngOnInit(): void {
    this.loadMaintenances();
  }

  loadMaintenances() {
    this.maintenanceService.getAllMaintenanceRequest().subscribe({
      next: (maintenance) => {
        this.maintenance = this.processMaintenances(maintenance);
        this.filteredmaintenances = [...this.maintenance];
      },
      error: (error) => {
        console.error('Failed to load maintenance:', error);
      }
    });
  }

  // Process expense data to ensure consistent format
  processMaintenances(maintenance: any[]): MaintenanceRequest[] {
    return maintenance.map(maintenance => {
      // Derive status from isPaid and dueDate
      const status = this.determineMaintenanceStatus(maintenance);

      return {
        ...maintenance,
        status: status
      };
    });
  }

  determineMaintenanceStatus(maintenance: any): string {
    if (maintenance.isPaid) {
      return 'paid';
    }

    const dueDate = new Date(maintenance.dueDate);
    const today = new Date();

    return dueDate < today ? 'overdue' : 'pending';
  }

  getStatusClass(status: string): string {
    const baseClasses = "px-2 py-1 rounded-full text-sm capitalize";
    const statusClasses: Record<string, string> = {
      paid: "bg-green-100 text-green-800",
      pending: "bg-yellow-100 text-yellow-800",
      overdue: "bg-red-100 text-red-800",
    };

    return `${baseClasses} ${statusClasses[status.toLowerCase()] || "bg-gray-100 text-gray-800"
      }`;
  }

  handleSearch(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    const term = inputElement.value.toLowerCase();

    if (!term) {
      this.filteredmaintenances = [...this.maintenance];
      return;
    }

    this.filteredmaintenances = this.maintenance.filter(
      (maintenance) =>
        (maintenance.details && maintenance.details.toLowerCase().includes(term))

    );
  }

  handleEditAction(maintenance: MaintenanceRequest) {
    this.router.navigate(['maintenance'], {
      relativeTo: this.route,
      state: { maintenance }
    });
  }

  async handleDeleteAction(maintenance: MaintenanceRequest) {
    const isConfirmed = await this.confirmDialog.show({
      title: "Delete maintenance",
      message: "Are you sure you want to delete this maintenance?",
      confirmText: "Delete",
      cancelText: "Cancel",
      type: "danger",
    });

    if (isConfirmed) {
      this.maintenanceService.deleteMaintenance(maintenance.id).subscribe({
        next: () => {
          // Remove payment from both arrays to update UI
          this.maintenance = this.maintenance.filter(e => e.id !== maintenance.id);
          this.filteredmaintenances = this.filteredmaintenances.filter(e => e.id !== maintenance.id);
        },
        error: (error) => {
          console.error('Error deleting payment:', error);
        }
      });
    }
  }
}