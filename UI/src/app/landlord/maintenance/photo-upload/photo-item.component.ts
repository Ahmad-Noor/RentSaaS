import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Photo } from '../../../models/maintenance.types';
import { formatFileSize } from '../../../utils/file.utils';
// import { formatFileSize } from '../utils/file.utils';

@Component({
  selector: 'app-photo-item',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="relative group">
      <img 
        [src]="imageUrl" 
        [alt]="photo.name"
        class="w-full h-32 object-cover rounded"
      >
      <button
        type="button"
        (click)="remove.emit(photo)"
        class="absolute top-2 right-2 p-1 bg-white rounded-full shadow opacity-0 group-hover:opacity-100 transition-opacity"
      >
        <span class="material-icons text-red-500">close</span>
      </button>
      <div class="mt-1">
        <p class="text-xs text-gray-500">{{ formatFileSize(photo.size) }}</p>
      </div>
    </div>
  `
})
export class PhotoItemComponent {
  @Input() photo!: Photo;
  @Output() remove = new EventEmitter<Photo>();

  protected formatFileSize = formatFileSize;

  get imageUrl(): string {
    return URL.createObjectURL(this.photo.file);
  }
}