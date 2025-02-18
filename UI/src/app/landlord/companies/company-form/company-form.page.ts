import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Router, RouterLink, ActivatedRoute } from "@angular/router";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms"; 
import { CompanyCreate } from "../../../models/company-create";
import { UserService } from "../../../service/user.service";
import { CompanyService } from "../../../service/company.service";

@Component({
  selector: "app-company-form-page",
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: "./company-form.html",
})
export class CompanyFormPage implements OnInit {
  companyForm: FormGroup;
  isEditMode = false;
  loading = false;
  companyId?: number;

  constructor(
    private _companyService: CompanyService,
    private _router: Router,
    private _route: ActivatedRoute,
    private _userService: UserService

  ) {
    this.companyForm = new FormGroup({
      name: new FormControl(null, Validators.required),
      type: new FormControl(null),
      ein: new FormControl(null),
    });
  }

  ngOnInit(): void {
    const id = this._route.snapshot.paramMap.get("id");
    if (id) {
      this.isEditMode = true;
      this.companyId = parseInt(id, 10);
      // this.loadCompany(this.companyId);
    }
  }

  onSubmit(companyForm:FormGroup): void {
    if (this.companyForm.invalid) {
      this.companyForm.markAllAsTouched();
      return;
    }

    this.loading = true; 
    const organizationId = this._userService.getCurrentOrganizationId();

    if (!organizationId) {
      console.error('Organization ID is missing.');
      this.loading = false;
      return;
    }

    const organization: CompanyCreate = {
      ...companyForm.value,
      organizationId: organizationId,
    };

    this._companyService.addCompany(organization).subscribe({
      next: (result) => {
        console.log("Company added successfully:", result);
        this._router.navigate(["/landlord/companies"]);
      },
      error: (error) => {
        console.error("Error adding company:", error);
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }

  // private loadCompany(id: number): void {
  //   this.loading = true;
  //   this.companyService.getCompany(id).subscribe({
  //     next: (company) => {
  //       if (company) {
  //         this.companyForm.patchValue(company);
  //       }
  //     },
  //     error: (error) => {
  //       console.error("Error loading company:", error);
  //     },
  //     complete: () => {
  //       this.loading = false;
  //     }
  //   });
  // }
}
