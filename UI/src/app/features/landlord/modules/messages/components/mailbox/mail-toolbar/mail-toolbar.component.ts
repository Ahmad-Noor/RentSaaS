import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-mail-toolbar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="bg-white border-b px-4 py-2 flex items-center justify-between">
      <!-- Left Actions -->
      <div class="flex items-center gap-2">
        <button class="p-2 hover:bg-gray-100 rounded-lg">
          <span class="material-icons">refresh</span>
        </button>
        <button class="p-2 hover:bg-gray-100 rounded-lg">
          <span class="material-icons">archive</span>
        </button>
        <button class="p-2 hover:bg-gray-100 rounded-lg text-red-600">
          <span class="material-icons">delete</span>
        </button>
      </div>

      <!-- Search -->
      <div class="flex-1 max-w-xl mx-4">
        <div class="relative">
          <span class="material-icons absolute left-3 top-2.5 text-gray-400">search</span>
          <input
            type="text"
            placeholder="Search messages"
            class="w-full pl-10 pr-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
        </div>
      </div>

      <!-- Right Actions -->
      <div class="flex items-center gap-2">
        <button class="p-2 hover:bg-gray-100 rounded-lg">
          <span class="material-icons">filter_list</span>
        </button>
        <button class="p-2 hover:bg-gray-100 rounded-lg">
          <span class="material-icons">more_vert</span>
        </button>
      </div>
    </div>
  `
})
export class MailToolbarComponent {}