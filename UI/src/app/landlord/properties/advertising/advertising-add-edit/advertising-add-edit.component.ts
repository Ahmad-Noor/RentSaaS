import { CommonModule } from "@angular/common";
import { RouterLink, Router, ActivatedRoute } from "@angular/router";
import { AdvertisingService } from "../../../../service/advertise.service";
import { Advertising } from "../../../../models/advertising.types";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { Company } from "../../../../models/company.types";
import { FileItemComponent } from "./file-item.component";
import { CompanyService } from "../../../../service/company.service";
import { PropertyService } from "../../../../service/property.service";
import { FileWithMetadata } from "../../../../models/fileWithMetadata.types";
import { EventEmitter, Input, Output, OnInit, Component } from "@angular/core";

@Component({
  selector: "app-advertising-add-edit",
  standalone: true,
  imports: [RouterLink, CommonModule, ReactiveFormsModule, FileItemComponent],
  templateUrl: "./advertising-add-edit.component.html",
  styleUrl: "./advertising-add-edit.component.css",
})
export class AdvertisingAddEditComponent implements OnInit {
  advertisingForm: FormGroup;
  @Input() ad?: Advertising;
  @Output() save = new EventEmitter<Advertising>();

  files: FileWithMetadata[] = [];
  filesToDelete: string[] = [];
  error = "";
  loading = false;
  properties: any[] = [];
  companies!: Company[];

  constructor(
    private _fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private _propertyService: PropertyService,
    private _advertisingService: AdvertisingService,
  ) {
    this.advertisingForm = this._fb.group({
      id: "",
      propertyId: new FormControl(null),
      monthlyRent: new FormControl(0, [Validators.required, Validators.min(0)]),
      securityDeposit: new FormControl(0, [Validators.required, Validators.min(0)]),
      details: new FormControl(""),
      availablefrom: new FormControl(null, [Validators.required]),
      leaseTerm: new FormControl("month-to-month", [Validators.required]),
      zillow: new FormControl(false),
      trulia: new FormControl(false),
      apartments: new FormControl(false),
      realtor: new FormControl(false),
      files: new FormControl([]),
    });
  }

  ngOnInit(): void {
    this.getAllProperties();

    const adData = history.state.ad;
    if (adData) {
      this.loadAdDetails(adData.id);
    }
  }

  loadAdDetails(adId: string): void {
    this._advertisingService.getAdvertisementById(adId).subscribe({
      next: (ad) => {
        const formValues: any = {};
        if (ad.availablefrom) {
          const availableFromDate = new Date(ad.availablefrom);
          if (!isNaN(availableFromDate.getTime())) {
            formValues.availableFrom = availableFromDate.toISOString().substring(0, 10);
          } else {
            console.warn("Invalid date format for availableFrom:", ad.availablefrom);
            formValues.availableFrom = null;
          }
        } else {
          formValues.availableFrom = null;
        }
        
        formValues.id = ad.id || "";
        formValues.propertyId = ad.propertyId || null;
        formValues.monthlyRent = ad.monthlyRent || 0;
        formValues.securityDeposit = ad.securityDeposit || 0;
        formValues.details = ad.details || "";
        formValues.leaseTerm = ad.leaseTerm || "month-to-month";
        formValues.zillow = ad.zillow || false;
        formValues.trulia = ad.trulia || false;
        formValues.apartments = ad.apartments || false;
        formValues.realtor = ad.realtor || false;
        formValues.files = ad.files || [];

        this.advertisingForm.patchValue(formValues);

        if (ad.files) {
          this.files = ad.files.map((file: any) => ({
            id: file.id,
            name: file.fileName,
            size: file.fileSize,
            type: file.fileName.endsWith(".pdf") ? "application/pdf" : "image/*",
            file: new File([], file.fileName),
            url: file.url,
          }));
        }
      },
      error: (err) => {
        console.error("Error loading ad details:", err);
      },
    });
  }

  onFormSubmit(): void {
    if (this.advertisingForm.valid) {
      const adData: Advertising = this.advertisingForm.value;

      if (adData.availablefrom) {
        const date = new Date(adData.availablefrom);
        if (!isNaN(date.getTime())) {
          // Convert to 'yyyy-mm-dd' format without time or timezone
        } else {
          this.error = "Invalid date format for availableFrom.";
          return;
        }
      } 

      const files = this.files.map((file) => file.file);
      adData.files = this.files;

      if (adData.id) {
        this._advertisingService
          .updateAdvertisement(adData.id, adData, files)
          .subscribe({
            next: (val: any) => {
              this.router.navigate([".."], { relativeTo: this.route });
            },
            error: (err: any) => {
              console.error("Error updating ad", err);
              if (err.error && err.error.message) {
                this.error = err.error.message;
              } else {
                this.error = "Failed to update ad. Please try again.";
              }
            },
          });
      } else {
        this._advertisingService.addAdvertisement(adData, files).subscribe({
          next: (val: any) => {
            this.router.navigate([".."], { relativeTo: this.route });
          },
          error: (err: any) => {
            console.error("Error adding ad", err);
            if (err.error && err.error.message) {
              this.error = err.error.message;
            } else {
              this.error = "Failed to add ad. Please try again.";
            }
          },
        });
      }
    } else {
      Object.keys(this.advertisingForm.controls).forEach((key) => {
        const control = this.advertisingForm.get(key);
        control?.markAsTouched();
      });
    }
  }

  getAllProperties() {
    this._propertyService.getAllProperties().subscribe({
      next: (properties: any) => {
        this.properties = properties.data;
      },
      error: (properties) => {},
      complete: () => {},
    });
  }

  onFilesSelected(event: any): void {
    const files = Array.from((event.target as HTMLInputElement).files || []);

    if (this.files.length + files.length > 20) {
      this.error = "You can upload a maximum of 20 files";
      return;
    }

    this.error = "";

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
  const validTypes = ["image/jpeg", "image/png"];
  const maxSize = 5 * 1024 * 1024;

  if (!validTypes.includes(file.type)) {
    return {
      isValid: false,
      error: "Invalid file type. Please upload JPG or PNG only.",
    };
  }

  if (file.size > maxSize) {
    return {
      isValid: false,
      error: "File size exceeds 5MB limit.",
    };
  }

  return { isValid: true };
}