import { Component } from '@angular/core';

@Component({
  selector: 'app-team-page',
  standalone: true,
  template: `
    <div class="p-4">
      <h1 class="text-2xl font-bold mb-4">Team Management</h1>
      <div class="grid grid-cols-1 gap-4">
        <div class="bg-white p-4 rounded-lg shadow">
          <h2 class="text-lg font-semibold mb-2">Team Members</h2>
          <!-- Team members list -->
        </div>
        <div class="bg-white p-4 rounded-lg shadow">
          <h2 class="text-lg font-semibold mb-2">Roles & Permissions</h2>
          <!-- Roles management -->
        </div>
      </div>
    </div>
  `
})
export class TeamPage {}