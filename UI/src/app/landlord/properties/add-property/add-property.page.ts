import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Router } from "@angular/router";
import { RouterLink } from "@angular/router";
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { PropertyService } from "../../../service/property.service";
import { Property } from "../../../models/property.types";

@Component({
  selector: "app-add-property-page",
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: "./add-property.page.html",
  styleUrls: ["./add-property.page.css"],
})
export class AddPropertyPage {
  constructor(
    private router: Router,
    private _propertyServices: PropertyService
  ) {}

  loading = false;
  error: string = "";
  propertyForm = new FormGroup({
    address: new FormControl(null, [
      Validators.required,
      Validators.minLength(10),
    ]),
  });

  CreateNewProperty(form: FormGroup) {
    if (form.valid) {
      this.loading = true;

      const property: Property = {
        address: form.get("address")?.value,
        createdBy: "00000000-0000-0000-0000-000000000001".trim(),
      };

      this._propertyServices.addProperty(property).subscribe({
        next: (responseSuccess) => {
          this.router.navigate(["/landlord/properties"]);
        },
        error: (response) => {
          this.error = response.error.message;
        },
        complete: () => {
          this.loading = false;
        },
      });
    }
  }
}
