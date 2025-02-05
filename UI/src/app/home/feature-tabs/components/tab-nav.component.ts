import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-tab-nav',
    imports: [CommonModule],
    template: `
    <nav class="flex border-b border-gray-200">
      @for (tab of tabs; track tab.id) {
        <button
          (click)="onTabSelect.emit(tab.id)"
          class="px-6 py-3 text-sm font-medium transition-colors border-b-2 -mb-[2px]"
          [class.border-blue-500]="activeTabId === tab.id"
          [class.text-blue-600]="activeTabId === tab.id"
          [class.border-transparent]="activeTabId !== tab.id"
          [class.text-gray-500]="activeTabId !== tab.id"
          [class.hover:text-gray-700]="activeTabId !== tab.id"
        >
          {{ tab.label }}
        </button>
      }
    </nav>
  `
})
export class TabNavComponent {
  @Input() tabs: Array<{ id: string; label: string }> = [];
  @Input() activeTabId = '';
  @Output() onTabSelect = new EventEmitter<string>();
}