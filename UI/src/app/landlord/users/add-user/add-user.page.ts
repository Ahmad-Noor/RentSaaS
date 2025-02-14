import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { UserFormComponent } from '../user-form/user-form.component';
import { UserService } from '../../../service/user.service';
import { CreateUserDTO } from '../../../models/user.types';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-user-page',
  standalone: true,
  imports: [CommonModule, RouterLink, UserFormComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Add New User</h1>
          <p class="mt-1 text-gray-600">Create a new user account and set their permissions</p>
        </div>
        <a 
          routerLink=".."
          class="text-gray-600 hover:text-gray-900 flex items-center gap-2"
        >
          <span class="material-icons">arrow_back</span>
          Back to Users
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-user-form (submit)="handleSubmit($event)" />
        </div>
      </div>
    </div>
  `
})
export class AddUserPage {
  constructor(
    private userService: UserService,
    private router: Router
  ) {}

  handleSubmit(data: CreateUserDTO): void {
    this.userService.createUser(data);
    this.router.navigate(['/landlord/users']);
  }
}