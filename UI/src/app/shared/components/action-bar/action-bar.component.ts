import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-action-bar',
    imports: [CommonModule, FormsModule],
    template: `
    <div class="flex items-center justify-between mb-4">
      <div class="relative">
        <span class="material-icons absolute left-3 top-2 text-gray-400">search</span>
        <input 
          type="text"
          [placeholder]="searchPlaceholder"
          [(ngModel)]="searchTerm"
          (ngModelChange)="onSearch.emit($event)"
          class="pl-10 pr-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
        >
      </div>
      <div class="flex gap-2">
        <button class="p-2 text-gray-600 hover:bg-gray-100 rounded">
          <span class="material-icons">filter_list</span>
        </button>
        <button class="p-2 text-gray-600 hover:bg-gray-100 rounded">
          <span class="material-icons">download</span>
        </button>
      </div>
    </div>
  `
})
export class ActionBarComponent {
  @Input() searchPlaceholder = 'Search...';
  @Output() onSearch = new EventEmitter<string>();
  searchTerm = '';
}