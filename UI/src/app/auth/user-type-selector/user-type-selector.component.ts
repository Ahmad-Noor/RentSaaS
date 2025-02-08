import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-type-selector',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-type-selector.component.html',
  styleUrls: ['./user-type-selector.component.css']
})
export class UserTypeSelectorComponent implements OnInit {
  @Input() formGroup!: FormGroup;

  ngOnInit(): void {
    // Ensure default value is set if it's not already defined
    if (!this.formGroup.controls['userType'].value) {
      this.formGroup.patchValue({ userType: 'tenant' });
    }
  }
}
