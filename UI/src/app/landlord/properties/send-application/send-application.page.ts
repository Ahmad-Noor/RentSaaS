import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router'; 
import { ApplicationFormComponent } from './add-application/application-form.component';

@Component({
  selector: 'app-send-application-page',
  standalone: true,
  imports: [CommonModule, RouterLink, ApplicationFormComponent],
  templateUrl:"send-application.page.html"
})
export class SendApplicationPage {
  constructor(private router: Router) {}

  handleSubmit(data: any): void {
    console.log('Application data:', data);
    // TODO: Implement application sending logic
    this.router.navigate(['/landlord/properties/applications']);
  }
}