import { Component } from '@angular/core';

@Component({
  selector: 'app-users-page',
  standalone: true,
  template: `
    <div class="p-4">
      <h1 class="text-2xl font-bold mb-4">User Management</h1>
      <div class="grid grid-cols-1 gap-4">
        <div class="bg-white p-4 rounded-lg shadow">
          <h2 class="text-lg font-semibold mb-2">Users</h2>
          <!-- Users list -->
        </div>
        <div class="bg-white p-4 rounded-lg shadow">
          <h2 class="text-lg font-semibold mb-2">Access Control</h2>
          <!-- Access management -->
        </div>
      </div>
    </div>
  `
})
export class UsersPage {}