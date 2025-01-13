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
import { TypeUser } from "../../../../shared/enums/type-user";

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
    firstName: new FormControl(null, [Validators.required]),
    lastName: new FormControl(null, [Validators.required]),
    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [
      Validators.required,
      Validators.minLength(8),
    ]),
    userType: new FormControl(TypeUser.Tenant, [Validators.required]),
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
    const formData = {
      ...registerForm.value,
      userType: Number(registerForm.get("userType")?.value),
    };
    if (this.registerForm.valid) {
      this._AuthService.Register(formData).subscribe({
        next: (response) => {
          console.log(response);
          this.router.navigate(["/login"]);
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {},
      });
    }
  }
}
