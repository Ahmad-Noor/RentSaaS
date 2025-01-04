import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search-bar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="relative">
      <span class="material-icons absolute left-3 top-2 text-gray-400">search</span>
      <input 
        type="text"
        [placeholder]="placeholder"
        [(ngModel)]="searchTerm"
        (ngModelChange)="onSearch.emit($event)"
        class="pl-10 pr-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
      >
    </div>
  `
})
export class SearchBarComponent {
  @Input() placeholder = 'Search...';
  @Output() onSearch = new EventEmitter<string>();
  searchTerm = '';
}