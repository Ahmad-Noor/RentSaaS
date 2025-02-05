import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-view-toggle',
  standalone: true,
  template: `
    <div class="flex items-center gap-2">
      <button 
        (click)="onViewChange('list')"
        [class]="getButtonClass('list')"
      >
        <span class="material-icons">view_list</span>
      </button>
      <button 
        (click)="onViewChange('grid')"
        [class]="getButtonClass('grid')"
      >
        <span class="material-icons">grid_view</span>
      </button>
    </div>
  `
})
export class ViewToggleComponent {
  @Input() currentView: 'list' | 'grid' = 'list';
  @Output() viewChange = new EventEmitter<'list' | 'grid'>();

  getButtonClass(view: 'list' | 'grid'): string {
    const baseClass = 'p-2 rounded transition-colors';
    return view === this.currentView
      ? `${baseClass} bg-blue-100 text-blue-600`
      : `${baseClass} text-gray-600 hover:bg-gray-100`;
  }

  onViewChange(view: 'list' | 'grid'): void {
    this.viewChange.emit(view);
  }
}