import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router'; 
import { ActionBarComponent } from '../../../shared/components/action-bar/action-bar.component';
import { ViewToggleComponent } from '../view-toggle/view-toggle.component';
import { PropertyCardComponent } from '../property-card/property-card.component';
import { ConfirmDialogService } from '../../../shared/services/confirm-dialog.service'; 
import { Property } from '../../../models/property.types';
import { PropertyService } from '../../../service/property.service';
import { PropertyTableComponent } from '../data-table/data-table.component';

@Component({
  selector: "app-properties-list-page",
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    PropertyTableComponent,
    ActionBarComponent,
    ViewToggleComponent,
    PropertyCardComponent,
  ],
  templateUrl: "./properties-list.page.html",
  styleUrls: ["./properties-list.page.css"],
})
export class PropertiesListPage {
  getStatusClass(arg0: boolean) {
    throw new Error("Method not implemented.");
  }
  view: "list" | "grid" = "list";
  properties!: Property[];
  onAction: any;

  constructor(
    private router: Router,
    private confirmDialog: ConfirmDialogService,
    private propertyService: PropertyService
  ) {
    this.propertyService.getAllProperties().subscribe((response) => {
      this.properties = response.data;
      console.log(response.data)
    });

    console.log(this.properties)




  }

  async handleAction(action: { type: string; property: Property }) {
    switch (action.type) {
      case "edit":
        this.router.navigate([
          "landlord",
          "properties",
          action.property.id,
          "edit",
        ]);
        break;

      case "advertise":
        this.router.navigate(["landlord", "properties", "advertising"]);
        break;

      case "maintenance":
        this.router.navigate([
          "landlord",
          "properties",
          action.property.id,
          "maintenance",
        ]);
        break;

      case "delete":
        const confirmed = await this.confirmDialog.show({
          title: "Delete Property",
          message: `Are you sure you want to delete ${action.property.address}?`,
          confirmText: "Delete",
          cancelText: "Cancel",
          type: "danger",
        });

        if (confirmed) {
          // TODO: Implement property deletion

          this.propertyService
            .Delete(
              action.property
                .id as `${string}-${string}-${string}-${string}-${string}`
            )
            .subscribe({
              next: () => {
                this.router.navigate(["/landlord/properties"]);
                console.log("Delete Succes Now");
              },
            });
          console.log("Deleting property:", action.property);
        }
        break;
    }
  }
}