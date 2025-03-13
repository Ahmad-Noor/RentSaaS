import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterLink, Router, ActivatedRoute } from "@angular/router";
import { Advertising } from "../../../../models/advertising.types";
import { AdvertisingService } from "../../../../service/advertise.service";
import { PropertyService } from "../../../../service/property.service";
import { ConfirmDialogService } from "../../../../shared/services/confirm-dialog/confirm-dialog.service";

@Component({
  selector: "app-advertising-page",
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: "./advertising.page.html",
})
export class AdvertisingPage implements OnInit {
  advertisements: Advertising[] = [];
  filteredAdvertisements: Advertising[] = [];
  properties: any[] = [];
  @Output() onAction = new EventEmitter<{ type: string; ad: Advertising }>();

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private advertisingService: AdvertisingService,
    private propertyService: PropertyService,
    private confirmDialog: ConfirmDialogService
  ) {}

  ngOnInit(): void {
    this.loadProperties();
    this.loadAdvertisements();
  }

  loadProperties() {
    this.propertyService.getAllProperties().subscribe({
      next: (properties) => {
        this.properties = properties.data;
      },
      error: (error) => {
        console.error('Failed to load properties:', error);
      }
    });
  }

  loadAdvertisements() {
    this.advertisingService.getAllAdvertisements().subscribe({
      next: (advertisements) => {
        this.advertisements = this.processAdvertisements(advertisements);
        this.filteredAdvertisements = [...this.advertisements];
      },
      error: (error) => {
        console.error('Failed to load advertisements:', error);
      }
    });
  }

  processAdvertisements(advertisements: any[]): Advertising[] {
    return advertisements.map(ad => {
      const status = this.determineAdStatus(ad);

      if (ad.availableFrom) {
        ad.availableFrom = new Date(ad.availableFrom);
      }

      return {
        ...ad,
        status: status
      };
    });
  }

  determineAdStatus(ad: any): string {
    if (!ad.availableFrom) {
      return 'pending';
    }

    const availableDate = new Date(ad.availableFrom);
    const today = new Date();

    if (availableDate > today) {
      return 'upcoming';
    } else if (availableDate <= today) {
      return 'active';
    }

    return 'expired';
  }

  getPropertyName(propertyId: number): string {
    const property = this.properties.find(p => p.id === propertyId);
    return property ? property.address : 'Unknown Property';
  }

  getStatusClass(status: string): string {
    const baseClasses = "px-2 py-1 rounded-full text-sm capitalize";
    const statusClasses: Record<string, string> = {
      active: "bg-green-100 text-green-800",
      upcoming: "bg-yellow-100 text-yellow-800",
      expired: "bg-red-100 text-red-800",
      pending: "bg-gray-100 text-gray-800"
    };

    return `${baseClasses} ${
      statusClasses[status.toLowerCase()] || "bg-gray-100 text-gray-800"
    }`;
  }

  handleSearch(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    const term = inputElement.value.toLowerCase();

    if (!term) {
      this.filteredAdvertisements = [...this.advertisements];
      return;
    }

    this.filteredAdvertisements = this.advertisements.filter(
      (ad) =>
        (ad.details && ad.details.toLowerCase().includes(term))
    );
  }

  handleEditAction(ad: Advertising) {
    this.router.navigate(['advertising-add-edit'], {
      relativeTo: this.route,
      state: { ad }
    });
  }

  async handleDeleteAction(ad: Advertising) {
    const isConfirmed = await this.confirmDialog.show({
      title: "Delete Advertisement",
      message: "Are you sure you want to delete this advertisement?",
      confirmText: "Delete",
      cancelText: "Cancel",
      type: "danger",
    });

    if (isConfirmed) {
      this.advertisingService.deleteAdvertisement(ad.id!).subscribe({
        next: () => {
          this.advertisements = this.advertisements.filter(a => a.id !== ad.id);
          this.filteredAdvertisements = this.filteredAdvertisements.filter(a => a.id !== ad.id);
        },
        error: (error) => {
          console.error('Error deleting advertisement:', error);
        }
      });
    }
  }
}