import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';
import {  FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PropertyService } from '../../services/property.service';


@Component({
  selector: "app-add-property-page",
  standalone: true,
  imports: [CommonModule, RouterLink,ReactiveFormsModule],
  templateUrl: "./add-property.page.html",
  styleUrls: ["./add-property.page.css"],
})
export class AddPropertyPage {
  constructor(
    private router: Router,
    private _propertyServices: PropertyService
  ) {}
  
  loading = false;
  error:string='';
  propertyForm = new FormGroup({
    address: new FormControl(null, [Validators.required,Validators.minLength(10),
      Validators.pattern("^(\\d+)\\s([A-Za-z0-9\\s]+),?\\s([A-Za-z\\s]+),\\s([A-Z]{2})\\s(\\d{5})(-\\d{4})?$")
    ]),
    unite: new FormControl("", [Validators.required, Validators.minLength(4)]),
    note: new FormControl(null),
  });

  CreateNewProperty(form: FormGroup) {
    if (form.valid) {
      this.loading=true;

      const PropertyDataAll = {
        address: form.get("address")?.value,
        note: form.get("note")?.value?.trim(),
        unite: form.get("unite")?.value,
        organizationId: localStorage.getItem("orgnaizationId"),
        createdAt: new Date().toISOString(),
        createdBy: "00000000-0000-0000-0000-000000000001".trim(),
        isDeleted: false,
        lastModifiedAt: null,
        lastModifiedBy: null,
        deletedAt: null,
        deletedBy: null,
      };

      console.log(PropertyDataAll);
      console.log(localStorage.getItem("orgnaizationId"));
      this._propertyServices.CreateNewProperty(PropertyDataAll).subscribe({
        next:(responceSuccess)=>{

         this.router.navigate(['/landlord/properties'])
        },


        error:(responce)=>{
this.error=responce.error.message
    
        },

        complete:()=>{

          this.loading=false;
        }



      });
    }
  }

}