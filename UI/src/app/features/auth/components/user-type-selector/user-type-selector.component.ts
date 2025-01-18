import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'app-user-type-selector',
    imports: [CommonModule, ReactiveFormsModule],
templateUrl:'./user-type-selector.component.html',
styleUrl:'./user-type-selector.component.css'
})
export class UserTypeSelectorComponent {
  @Input() formGroup!: FormGroup;
}


