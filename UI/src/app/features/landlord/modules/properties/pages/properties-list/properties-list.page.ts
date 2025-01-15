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
  templateUrl: './properties-list.page.html',
  styleUrls: ['./properties-list.page.css']
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