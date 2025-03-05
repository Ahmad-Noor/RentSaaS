import { CommonModule } from "@angular/common";
import { RouterLink, Router, ActivatedRoute } from "@angular/router";
import { MaintenanceService } from "../../../../service/maintenance.service";
import { MaintenanceFormData } from "../../../../models/Maintenance-FormData.type";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { MaintenanceRequest } from "../../../../models/maintenance.types";
import { FileItemComponent } from "./file-item.component";
import { PropertyService } from "../../../../service/property.service";

import { FileWithMetadata } from "../../../../models/fileWithMetadata.types";
import { EventEmitter, Input, Output, OnInit, Component } from "@angular/core";

@Component({
  selector: 'app-maintenance-add-edit',
  imports: [RouterLink, CommonModule, ReactiveFormsModule, FileItemComponent],
  templateUrl: './maintenance-add-edit.component.html',
  styleUrl: './maintenance-add-edit.component.css'
})
export class MaintenanceAddEditComponent {
  maintentenceForm: FormGroup;
  @Input() maintenance?: MaintenanceRequest;
  @Output() save = new EventEmitter<MaintenanceFormData>();

  files: FileWithMetadata[] = [];
  filesToDelete: string[] = [];
  error = "";
  loading = false;
  properties: any[] = [];

  constructor(
    private _fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private _propertyService: PropertyService,
    private _maintenanceService: MaintenanceService,

  ) {
    this.maintentenceForm = this._fb.group({
      id: "",
      propertyId: new FormControl(null),
      dueDate: new FormControl(new Date().toISOString().substring(0, 10), [
        Validators.required,
      ]),
      details: new FormControl(null),
      type: new FormControl("property"),
      files: new FormControl([]),
    });
  }

  ngOnInit(): void {
    this.getAllProperties();

    // Check if we're editing an existing expense
    const maintenanceData = history.state.maintenance;
    if (maintenanceData) {
      this.loadMaintenanceDataDetails(maintenanceData.id);
    }
  }

  loadMaintenanceDataDetails(maintenanceId: string): void {
    this._maintenanceService.getMaintenanceById(maintenanceId).subscribe({
      next: (maintenance) => {
        if (maintenance.dueDate) {
          maintenance.dueDate = new Date(maintenance.dueDate)
            .toISOString()
            .substring(0, 10);
        }
        this.maintentenceForm.patchValue(maintenance);

        if (maintenance.files) {
          this.files = maintenance.files.map((files: any) => ({
            id: files.id,
            name: files.fileName,
            size: files.fileSize,
            type: files.fileName.endsWith(".pdf")
              ? "application/pdf"
              : "image/*",
            file: new File([], files.fileName),
            url: files.url,
          }));
        }
      },
      error: (err) => {
        console.error("Error loading maintenance details:", err);
      },
    });
  }
  onFormSubmit(): void {
    if (this.maintentenceForm.valid) {
      const maintenanceData = this.maintentenceForm.value;

      // Format date for API
      if (maintenanceData.dueDate) {
        const date = new Date(maintenanceData.dueDate);
        if (!isNaN(date.getTime())) {
          maintenanceData.dueDate = date.toISOString().split("T")[0];
        }
      }

      // Prepare file files
      const files = this.files.map((file) => file.file);
      maintenanceData.filesToDelete = this.filesToDelete;

      // Update or create expense
      if (maintenanceData.id) {
        this._maintenanceService
          .updateMaintenance(maintenanceData.id, maintenanceData, files)
          .subscribe({
            next: (val: any) => {
              this.router.navigate([".."], { relativeTo: this.route });
            },
            error: (err: any) => {
              console.error("Error updating maintenance", err);
              // Add error handling here to display to user
            },
          });
      } else {
        // For adding new maintenance
        this._maintenanceService.addMaintenance(maintenanceData, files).subscribe({
          next: (val: any) => {
            this.router.navigate([".."], { relativeTo: this.route });
          },
          error: (err: any) => {
            console.error("Error adding maintenance", err);
            if (err.error && err.error.message) {
              this.error = err.error.message;
            } else {
              this.error = "Failed to add maintenance. Please try again.";
            }
          },
        });
      }
    } else {
      // Mark all form controls as touched to show validation errors
      Object.keys(this.maintentenceForm.controls).forEach((key) => {
        const control = this.maintentenceForm.get(key);
        control?.markAsTouched();
      });
    }
  }

  getAllProperties() {
    this._propertyService.getAllProperties().subscribe({
      next: (properties: any) => {
        this.properties = properties.data;
      },
      error: (properties) => { },
      complete: () => { },
    });
  }

  onFilesSelected(event: any): void {
    const files = Array.from((event.target as HTMLInputElement).files || []);

    if (this.files.length + files.length > 5) {
      this.error = "You can upload a maximum of 5 files";
      return;
    }

    this.error = ""; // Clear any previous errors

    files.forEach((fileInfo: any) => {
      const validation = validateFileWithMetadata(fileInfo);
      if (!validation.isValid) {
        this.error = validation.error || "Invalid file";
        return;
      }

      const file: FileWithMetadata = {
        id: crypto.randomUUID(),
        file: fileInfo,
        name: fileInfo.name,
        size: fileInfo.size,
        type: fileInfo.type,
      };

      this.files.push(file);
    });

    // Reset the file input
    (event.target as HTMLInputElement).value = "";
  }

  removeFile(file: any): void {
    this.files = this.files.filter((r) => r.id !== file.id);
    this.filesToDelete.push(file.id);
    this.error = "";
  }
}

function validateFileWithMetadata(file: FileWithMetadata): {
  isValid: boolean;
  error?: string;
} {
  const validTypes = ["image/jpeg", "image/png", "application/pdf"];
  const maxSize = 5 * 1024 * 1024; // 5MB

  if (!validTypes.includes(file.type)) {
    return {
      isValid: false,
      error: "Invalid file type. Please upload JPG, PNG or PDF only.",
    };
  }

  if (file.size > maxSize) {
    return {
      isValid: false,
      error: "FileWithMetadata size exceeds 5MB limit.",
    };
  }

  return { isValid: true };
}