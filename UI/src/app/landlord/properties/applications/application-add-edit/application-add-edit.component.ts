import { Component, EventEmitter, Input, Output, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ApplicationService } from "../../../../service/application.service";
import { PropertyService } from "../../../../service/property.service";
import { Application, ApplicationFormData } from "../../../../models/application.types";
import { CommonModule } from "@angular/common";

@Component({
  selector: "app-application-add-edit",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: "./application-add-edit.component.html",
  styleUrls: ["./application-add-edit.component.css"],
})
export class ApplicationAddEditComponent implements OnInit {
  @Input() application?: Application;
  @Output() save = new EventEmitter<ApplicationFormData>();

  applicationForm: FormGroup;
  error = "";
  loading = false;
  properties: any[] = [];

  constructor(
    private fb: FormBuilder,
    private applicationService: ApplicationService,
    private propertyService: PropertyService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.applicationForm = this.fb.group({
      id: "",
      propertyId: new FormControl(null),
      // applicantEmail: [null, [Validators.required, Validators.email]],
      applicantEmail: new FormControl(null, [Validators.required, Validators.email]),

      phoneNumber: new FormControl(null, Validators.required),
      message: new FormControl(true),
      requestbackgroundcheck: new FormControl(true, Validators.required),
      requestcreditreport: new FormControl(true, Validators.required),
    });
  }

  ngOnInit(): void {
    this.getAllProperties();
    const applicationData = history.state.application;
    console.log(applicationData);
    if (applicationData) {
      this.loadApplicationDetails(applicationData.id);
    }
  }

  loadApplicationDetails(applicationId: string): void {
    this.applicationService.getApplicationById(applicationId).subscribe({
      next: (application) => {
        console.log(application);
        this.applicationForm.patchValue(application);
      },
      error: (err) => {
        console.error("Error loading application details:", err);
      },
    });
  }

  onFormSubmit(): void {
    if (this.applicationForm.valid) {
      const applicationData = this.applicationForm.value;
      this.loading = true;

      if (this.application?.id) {
        this.applicationService.updateApplication(this.application.id, applicationData).subscribe({
          next: () => this.router.navigate([".."], { relativeTo: this.route }),
          error: (err) => this.handleError(err),
        });
      } else {
        this.applicationService.addApplication(applicationData).subscribe({
          next: () => this.router.navigate([".."], { relativeTo: this.route }),
          error: (err) => this.handleError(err),
        });
      }
    } else {
      this.markFormControlsTouched();
    }
  }

  deleteApplication(): void {
    if (this.application?.id) {
      this.applicationService.deleteApplication(this.application.id).subscribe({
        next: () => this.router.navigate([".."], { relativeTo: this.route }),
        error: (err) => this.handleError(err),
      });
    }
  }

  getAllProperties(): void {
    this.propertyService.getAllProperties().subscribe({
      next: (response) => {
        this.properties = response.data;
      },
      error: (err) => console.error("Error fetching properties:", err),
    });
  }

  private handleError(err: any): void {
    console.error("Error:", err);
    this.error = err.error?.message || "An error occurred. Please try again.";
    this.loading = false;
  }

  private markFormControlsTouched(): void {
    Object.keys(this.applicationForm.controls).forEach((key) => {
      this.applicationForm.get(key)?.markAsTouched();
    });
  }
}
