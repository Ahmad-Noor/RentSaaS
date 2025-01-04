import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { Router } from "@angular/router";
import { UserTypeSelectorComponent } from "../user-type-selector/user-type-selector.component";
import { FormFieldComponent } from "../../../../shared/components/form-field/form-field.component";
import { ButtonComponent } from "../../../../shared/components/button/button.component";
import { getFieldErrorMessage } from "../../utils/form-errors.utils";
import { AuthService } from "../../services/auth.service";

@Component({
  selector: "app-register-form",
  imports: [
    CommonModule,
    ReactiveFormsModule,
    UserTypeSelectorComponent,
    FormFieldComponent,
    ButtonComponent,
    
  ],
  standalone: true,
  templateUrl: "./register-form.component.html",
  styleUrls: ["./register-form.component.css"],
})
export class RegisterFormComponent {
  registerForm = new FormGroup({
    firstName: new FormControl("", [Validators.required]),
    lastName: new FormControl("", [Validators.required]),
    email: new FormControl("", [Validators.required, Validators.email]),
    password: new FormControl("", [
      Validators.required,
      Validators.minLength(8),
    ]),
    organizationId: new FormControl("", [Validators.required]),
    terms: new FormControl(false, [Validators.requiredTrue]),
  });

  constructor(private router: Router, private _AuthService: AuthService) {}

  getFieldError(field: string): string {
    const control = this.registerForm.get(field);
    if (control?.touched && control.errors) {
      return getFieldErrorMessage(field, control.errors);
    }
    return "";
  }

  onSubmit(registerForm: FormGroup): void {
    if (this.registerForm.valid) {
      this._AuthService.Register(registerForm.value).subscribe({
        next: (response) => {
          this.router.navigate(["/login"]);
        },
        error: (error) => {},
        complete: () => {},
      });
    }
  }
}
