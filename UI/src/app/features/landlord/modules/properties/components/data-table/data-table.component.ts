import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Property } from '../../types/property.types';
import { PropertyActionsComponent } from '../property-actions/property-actions.component';
import { GetAllService } from '../../services/property/get-all.service';

@Component({
  selector: 'app-property-table',
  standalone: true,
  imports: [CommonModule, PropertyActionsComponent],
templateUrl: './data-table.component.html',
styleUrls: ['./data-table.component.css']
})
export class PropertyTableComponent {
  @Input() properties: Property[] = [];
  @Output() onAction = new EventEmitter<{ type: string; property: Property }>();



  

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm';
    const statusClasses: Record<string, string> = {
      'Active': 'bg-green-100 text-green-800',
      'Inactive': 'bg-gray-100 text-gray-800',
      'Pending': 'bg-yellow-100 text-yellow-800'
    };

    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }


constructor(private _X:GetAllService) { }

  ngOnInit(){
    this._X.getAllProperties().subscribe((res) => {
    console.log(res);
    });
    
  }



}