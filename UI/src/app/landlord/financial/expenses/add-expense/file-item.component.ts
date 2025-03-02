import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileWithMetadata } from '../../../../models/fileWithMetadata.types';

@Component({
  selector: 'app-file-item',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="flex items-center justify-between p-2 border rounded">
      <div class="flex items-center gap-2">
        <span class="material-icons text-gray-400">
          {{ fileWithMetadata.type.includes('pdf') ? 'picture_as_pdf' : 'image' }}
        </span>
        <div>
          <p class="text-sm font-medium">{{ fileWithMetadata.name }}</p>
          <p class="text-xs text-gray-500">{{ formatSize(fileWithMetadata.size) }}</p>
        </div>
      </div>
      <button type="button" (click)="onRemove()" class="text-gray-400 hover:text-red-500">
        <span class="material-icons">close</span>
      </button>
    </div>
  `
})
export class FileItemComponent {
  @Input() fileWithMetadata!: FileWithMetadata;
  @Output() remove = new EventEmitter<FileWithMetadata>();

  formatSize(size: number): string {
    return `${(size / 1024).toFixed(2)} KB`;
  }
  onRemove(): void {
    this.remove.emit(this.fileWithMetadata);
  }
}