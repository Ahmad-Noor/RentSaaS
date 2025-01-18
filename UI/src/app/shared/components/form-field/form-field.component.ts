import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-form-field',
    imports: [CommonModule],
    templateUrl: './form-field.component.html',
    styleUrls: ['./form-field.component.css']
})
export class FormFieldComponent {
  @Input() label!: string;
  @Input() id!: string;
  @Input() error?: string;
}