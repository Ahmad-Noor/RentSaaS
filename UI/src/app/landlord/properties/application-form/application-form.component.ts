import { Component, EventEmitter, Inject, Output, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormFieldComponent } from '../../../shared/components/form-field/form-field.component'; 
//import { ApplicationSelectorComponent } from './Application-selector/Application-selector.component';
import { ApplicationService } from '../services/application.service';
import { Constant } from '../../../constants';

@Component({
  selector: 'app-application-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent,/*ApplicationSelectorComponent*/RouterLink],
  templateUrl:"./application-form.component.html"
})
export class ApplicationFormComponent {
  @Output() submit = new EventEmitter<any>();

  applicationForm: FormGroup;
  loading = false;
  orgid !:string;

  
  constructor(
    @Inject(PLATFORM_ID) private Checkplatform:object,
    private fb: FormBuilder,
    private router: Router,
    private _applicationServices: ApplicationService) {
    this.applicationForm = this.fb.group({
      ApplicationId: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      message: [''],
      requestBackground: [true],
      requestCredit: [true]
    });
    this.init()
  }


  init()
{
if (isPlatformBrowser(this.Checkplatform)) {
  const orgIdFromStorage = localStorage.getItem(Constant.OrganizationIdRentSass);
  if (orgIdFromStorage) {
    this.orgid =  orgIdFromStorage ;
  }
}

}



CreateNewApplication(form: FormGroup) {

  if (form.valid) {
    this.loading=true;

    const ApplicationDataAll = {
       
      organizationId: this.orgid,
      propertyId: form.get("ApplicationId")?.value,
      applicantEmail: form.get("email")?.value,
      phoneNumber: form.get("phone")?.value,
      message: form.get("message")?.value,
      requestBackgroundCheck: form.get("requestBackground")?.value,
      requestCreditReport: form.get("requestCredit")?.value,
      createdAt: new Date().toISOString(),
      createdBy: "00000000-0000-0000-0000-000000000001".trim(), // current user get from local storage 
      // isDeleted: false, // send null 
      // lastModifiedAt: null,
      // lastModifiedBy: null,
      // deletedAt: null,
      // deletedBy: null,
 

    };
    console.log(ApplicationDataAll);
     


    
    this._applicationServices.CreateNewApplication(ApplicationDataAll).subscribe({
      next:(responceSuccess)=>{
        console.log(responceSuccess);
       this.router.navigate(['/landlord/properties/applications'])
      },


      error:(responce)=>{
        console.log(responce);
      //    this.error=responce.error.message
  
      },

      complete:()=>{

        this.loading=false;
      }



    });
  }
}
  handleSubmit(): void {
    if (this.applicationForm.valid) {
      this.loading = true;
      this.submit.emit(this.applicationForm.value);
    }
  }
}


// <app-property-selector [formGroup]="applicationForm" />