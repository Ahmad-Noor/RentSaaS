import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';
import { PropertyFormComponent } from '../../components/property-form/property-form.component';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-property-page',
  standalone: true,
  imports: [CommonModule, RouterLink, PropertyFormComponent],
  templateUrl: './add-property.page.html',
    styleUrls: ['./add-property.page.css']
})
export class AddPropertyPage {








  constructor(private router: Router) {}


  handleSubmit(data: any): void {
    console.log('Property data:', data);
    // TODO: Save property data
    this.router.navigate(['/landlord/properties']);
  }
}