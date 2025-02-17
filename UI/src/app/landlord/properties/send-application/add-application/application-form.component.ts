import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormFieldComponent } from '../../../../shared/components/form-field/form-field.component'; 
import { PropertySelectorComponent } from '../../property-selector/property-selector.component';
import { ApplicationService } from '../../../../service/application.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-application-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent, PropertySelectorComponent],
  templateUrl:"application-form.component.html"
})
export class ApplicationFormComponent {
  @Output() submit = new EventEmitter<any>();

  applicationForm: FormGroup;
  loading = false;

  constructor(private fb: FormBuilder,private applicationsrv:ApplicationService , private _route:Router) {
    this.applicationForm = this.fb.group({
      propertyId: [null, Validators.required],
      applicantEmail: [null, [Validators.required, Validators.email]],
      phoneNumber: [null, Validators.required],
      message: [null],
      requestBackground: [true],
      requestCredit: [true]
    });
  }
 


  handleSubmit(): void {
    if (this.applicationForm.valid) {
      this.applicationsrv.addApplication(this.applicationForm.value).subscribe({
        next:(x)=>{
          console.log(x)
            this._route.navigate(['/landlord/properties/applications'])
        },
        error:(v)=>{console.log(v)},
        
      });
      this.loading = true;
      this.submit.emit(this.applicationForm.value);
    }
  }
}