import { CommonModule } from '@angular/common';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { EventEmitter,Input,Output,OnInit,Component} from "@angular/core";
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms'; 
import { FormFieldComponent } from '../../../../shared/components/form-field/form-field.component';
import { PropertySelectorComponent } from '../../property-selector/property-selector.component'; 
import { Lease } from '../../../../models/lease.types';
import { LeaseService } from '../../../../service/lease.service';
import { PropertyService } from "../../../../service/property.service"; 

@Component({
  selector: 'app-create-lease-page',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    ReactiveFormsModule,
    FormFieldComponent,
    PropertySelectorComponent
  ],
  templateUrl:"create-lease.page.html"
})
export class CreateLeasePage {
  leaseForm: FormGroup;
   @Input() lease?: Lease;
  @Output() save = new EventEmitter<Lease>();
  error = ""; 
  loading = false;
  properties: any[] = [];

  constructor(
    private fb: FormBuilder,
    private _propertyServices: PropertyService,
    private router: Router,
    private route: ActivatedRoute,
    private leaseService: LeaseService
  ) {
    this.getAllProperties();

    this.leaseForm = this.fb.group({
      propertyId: ['', Validators.required],
      tenantName: ['', Validators.required],
      leaseType: ['', Validators.required], // Changed from 'type'
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      monthlyRent: ['', [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit() {
    this.leaseForm.patchValue({
      type: this.lease?.leaseType || ''  // Ensure type has a default value
    });
  }

  getAllProperties() {
    this._propertyServices.getAllProperties().subscribe({
      next: (properties: any) => {        this.properties = properties.data;      },
      error: (properties) => {},
      complete: () => {},
    });
  }

  onFormSubmit(): void {
    if (this.leaseForm.valid) {
      const leaseData: Lease = {
        propertyId: this.leaseForm.value.propertyId,
        tenantName: this.leaseForm.value.tenantName,
        leaseType: this.leaseForm.value.type,  // Ensure this is assigned correctly
        startDate: this.leaseForm.value.startDate,
        endDate: this.leaseForm.value.endDate,
        // monthlyRent: this.leaseForm.value.monthlyRent
      };
  
      this.leaseService.addLease(leaseData).subscribe({
        next: () => {
          console.log('Lease added successfully'); 
          this.router.navigate(['..'], { relativeTo: this.route });
        },
        error: (err: any) => {
          console.error(err);
        }
      });
    }
  }

  handleSubmit(): void {
    if (this.leaseForm.valid) {
      this.loading = true;
      const leaseData: Lease = {
        propertyId: this.leaseForm.value.propertyId,
        tenantName: this.leaseForm.value.tenantName,
        leaseType: this.leaseForm.value.leaseType, // Changed from 'type'
        startDate: this.leaseForm.value.startDate,
        endDate: this.leaseForm.value.endDate,
        //monthlyRent: Number(this.leaseForm.value.monthlyRent)
      };

      this.leaseService.addLease(leaseData).subscribe({
        next: (response) => {
          console.log('Lease created successfully', response);
          this.loading = false;
          this.router.navigate(['..'], { relativeTo: this.route });
        },
        error: (error) => {
          console.error('Error creating lease:', error);
          this.loading = false;
          this.error = 'Failed to create lease';
        }
      });
    }
  }
}