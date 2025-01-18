import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';
import { PropertyFormComponent } from '../../components/property-form/property-form.component';
import {  FormGroup } from '@angular/forms';
import { PropertyService } from '../../services/property.service';


@Component({
  selector: "app-add-property-page",
  standalone: true,
  imports: [CommonModule, RouterLink, PropertyFormComponent],
  templateUrl: "./add-property.page.html",
  styleUrls: ["./add-property.page.css"],
})
export class AddPropertyPage {
  constructor(
    private router: Router,
    private _propertyServices: PropertyService
  ) {}

  CreateNewProperty(form: FormGroup) {

    if(form.valid)
    {
      console.log("Property data:", form);

      console.log("hambozo", form);
      const PropertyDataAll = {
        address: form.get('address')?.value,
        note: form.get('note')?.value?.trim(),
        unite:form.get('unite')?.value,
        organizationId: localStorage.getItem('organizationId')?.trim() || '',
        createdAt: new Date().toISOString(),
        createdBy: "00000000-0000-0000-0000-000000000001".trim(),
        lastModifiedAt: null,
        lastModifiedBy: null,
        isDeleted: false,
        deletedAt: null,
        deletedBy: null,
      };
  
      console.log(form);
      this._propertyServices.CreateNewProperty(form.value).subscribe((res) => {
        console.log(res);
      });
    }







  }

  handleSubmit(data: any): void {
    this._propertyServices
      .CreateNewProperty(data)
      .subscribe((res) => {
        console.log(res);
      });
  }
}