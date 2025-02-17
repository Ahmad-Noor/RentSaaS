import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Router } from "@angular/router";
import { RouterLink } from "@angular/router";
import { ActionBarComponent } from "../../../shared/components/action-bar/action-bar.component";
import { ViewToggleComponent } from "../view-toggle/view-toggle.component";
import { Editproperty } from "../edit-property/edit-property";
import { ConfirmDialogService } from "../../../shared/services/confirm-dialog.service";
import { Property } from "../../../models/property.model";
import { PropertyService } from "../../../service/property.service";
import { PropertyTableComponent } from "../data-table/data-table.component";

@Component({
  selector: "app-properties-list-page",
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    PropertyTableComponent,
    ActionBarComponent,
    ViewToggleComponent,
    Editproperty,
  ],
  templateUrl: "./properties-list.page.html",
  styleUrls: ["./properties-list.page.css"],
})
export class PropertiesListPage {
  getStatusClass(arg0: boolean) {
    throw new Error("Method not implemented.");
  }
  view: "list" | "grid" = "list";
  properties: Property[] = [];
  onAction: any;

  constructor(
    private router: Router,
    private confirmDialog: ConfirmDialogService,
    private propertyService: PropertyService
  ) {}

  ngOnInit() {
    this.loadProperties();
  }
  loadProperties() {
    this.propertyService.getAllProperties().subscribe({
      next: (response: any) => {
        if (response && response.data) {
          this.properties = response.data;
        }
      },
      error: (error) => {
        console.error("Error loading properties:", error);
      },
    });
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
          this.propertyService.deleteProperty(
              action.property.id as `${string}-${string}-${string}-${string}-${string}`
            )
            .subscribe({
              next: () => {
                this.router.navigate(["/landlord/properties"]); 
              },
            }); 
        }
        break;
    }
  }
}
