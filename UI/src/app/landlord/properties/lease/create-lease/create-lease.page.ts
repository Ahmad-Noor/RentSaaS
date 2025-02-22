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
      propertyId:  new FormControl(null, [Validators.required]),
      tenantName:  new FormControl(null, [Validators.required]),
      leaseType:  new FormControl(null, [Validators.required]),
      startDate:  new FormControl(null, [Validators.required]),
      endDate:  new FormControl(null, [Validators.required]),
      monthlyRent: new FormControl(null, [Validators.required, Validators.min(0)]) 
    });
  }

  getAllProperties() {
    this._propertyServices.getAllProperties().subscribe({
      next: (properties: any) => {        this.properties = properties.data;      },
      error: (properties) => {},
      complete: () => {},
    });
  }
  ngOnInit() {
    this.leaseForm.patchValue({
      type: this.lease?.leaseType || ''  // Ensure type has a default value
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
        propertyId: String(this.leaseForm.value.propertyId),
        tenantName: this.leaseForm.value.tenantName,
        leaseType: this.leaseForm.value.type,
        startDate: this.leaseForm.value.startDate,
        endDate: this.leaseForm.value.endDate,
        // monthlyRent: Number(this.leaseForm.value.monthlyRent)
      };

      this.leaseService.addLease(leaseData);
      this.router.navigate(['..'], { relativeTo: this.route });
    }
  }
}