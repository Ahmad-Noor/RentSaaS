import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';
import { PropertyTableComponent } from '../../components/data-table/data-table.component';
import { ActionBarComponent } from '../../../../../../shared/components/action-bar/action-bar.component';
import { ViewToggleComponent } from '../../components/view-toggle/view-toggle.component';
import { PropertyCardComponent } from '../../components/property-card/property-card.component';
import { Property } from '../../types/property.types';
import { ConfirmDialogService } from '../../../../../../shared/services/confirm-dialog/confirm-dialog.service';
import { PropertyService } from '../../services/property.service';

@Component({
  selector: 'app-properties-list-page',
  standalone: true,
  imports: [
    CommonModule, 
    RouterLink,
    PropertyTableComponent, 
    ActionBarComponent,
    ViewToggleComponent,
    PropertyCardComponent
  ],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <h1 class="text-2xl font-semibold">Properties</h1>
        <div class="flex items-center gap-4">
          <app-view-toggle
            [currentView]="view"
            (viewChange)="view = $event"
          />
          <a 
            routerLink="new"
            class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors"
          >
            <span class="material-icons text-sm">add</span>
            Add Property
          </a>
        </div>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-action-bar searchPlaceholder="Search properties" />
          
          @if (view === 'list') {
            <app-property-table
              [properties]="properties"
              (onAction)="handleAction($event)"
            />
          } @else {
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mt-6">
              @for (property of properties; track property.id) {
                <app-property-card
                  [property]="property"
                  (onAction)="handleAction($event)"
                />
              }
            </div>
          }
        </div>
      </div>
    </div>
  `
})
export class PropertiesListPage {
  view: 'list' | 'grid' = 'list';
  properties: Property[] = [];

  constructor(
    private router: Router,
    private confirmDialog: ConfirmDialogService,
    private propertyService: PropertyService
  ) {
    this.propertyService.getProperties().subscribe(properties => {
      this.properties = properties;
    });
  }

  async handleAction(action: { type: string; property: Property }) {
    switch (action.type) {
      case 'edit':
        this.router.navigate(['landlord', 'properties', action.property.id, 'edit']);
        break;
        
      case 'advertise':
        this.router.navigate(['landlord', 'properties', 'advertising']);
        break;

      case 'maintenance':
        this.router.navigate(['landlord', 'properties', action.property.id, 'maintenance']);
        break;

      case 'delete':
        const confirmed = await this.confirmDialog.show({
          title: 'Delete Property',
          message: `Are you sure you want to delete ${action.property.name}?`,
          confirmText: 'Delete',
          cancelText: 'Cancel',
          type: 'danger'
        });
        
        if (confirmed) {
          // TODO: Implement property deletion
          console.log('Deleting property:', action.property);
        }
        break;
    }
  }
}