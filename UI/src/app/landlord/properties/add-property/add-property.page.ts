import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ActivatedRoute, Router } from "@angular/router";
import { RouterLink } from "@angular/router";
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { PropertyService } from "../../../service/property.service";
import { UserService } from "../../../service/user.service";

@Component({
  selector: "app-add-property-page",
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: "./add-property.page.html",
  // styleUrls: ["./add-property.page.css"],
})
export class AddPropertyPage implements OnInit {
  propertyId: string | null = null;
  loading = false;
  error: string = "";

  propertyForm = new FormGroup({
    address: new FormControl(null, [
      Validators.required,
      Validators.minLength(10),
    ]),
  });

  constructor(
    private router: Router,
    private _propertyServices: PropertyService,
    private userService: UserService,
    private _activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.CheckUpdateOrCreate();
  }

  CheckUpdateOrCreate() {
    if (this._activatedRoute.snapshot.paramMap.get("id") != null) {
      this.propertyId = this._activatedRoute.snapshot.paramMap.get("id");
      if (this.propertyId) {
        this._propertyServices.getPropertyById(this.propertyId).subscribe({
          next: (responce) => {
            this.propertyForm.patchValue({
              address: responce.data.address,
            });
          },
        });
      }
    }
  }

  Submite(form: FormGroup) {
    if (this.propertyId != null) {
      console.log(this.propertyId);
      this.Update(form);
    } else {
      console.log(this, "propertyId");
      this.AddProperty(form);
    }
  }

  Update(form: FormGroup) {
    if (form.valid) {
      if (this.propertyId) {
        let property={
          address:form.get('address')?.value,
          id: this.propertyId as `${string}-${string}-${string}-${string}-${string}`
        }
        this._propertyServices
          .updateProperty(this.propertyId, property)
          .subscribe({
            next: (resulte) => {
              this.router.navigate(["/landlord/properties"]);
            },
            error: (resulte) => {
              console.log(resulte);
            },
          });
      } else {
      }
    } else {
      form.markAllAsTouched();
    }
  }
  AddProperty(form: FormGroup) {
    if (form.valid) {
      this.loading = true;
      this._propertyServices.addProperty(form.value).subscribe({
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
