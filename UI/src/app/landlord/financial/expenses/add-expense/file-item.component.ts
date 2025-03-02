import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileWithMetadata } from '../../../../models/fileWithMetadata.types';

@Component({
  selector: 'app-file-item',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="bg-white rounded-lg shadow-md overflow-hidden border border-gray-200 hover:shadow-lg transition-shadow">
      <!-- Preview section -->
      <div class="h-32 flex items-center justify-center bg-gray-100">
        <!-- PDF Preview -->
        <div *ngIf="isPdf" class="flex flex-col items-center justify-center">
          <span class="material-icons text-red-500 text-4xl">picture_as_pdf</span>
          <span class="text-xs text-gray-500 mt-1">PDF Document</span>
        </div>
        
        <!-- Image Preview -->
        <div *ngIf="isImage" class="w-full h-full">
          <img 
            *ngIf="imagePreviewUrl" 
            [src]="imagePreviewUrl" 
            alt="Preview" 
            class="w-full h-full object-cover"
          >
          <div *ngIf="!imagePreviewUrl" class="flex flex-col items-center justify-center h-full">
            <span class="material-icons text-blue-500 text-4xl">image</span>
            <span class="text-xs text-gray-500 mt-1">Image</span>
          </div>
        </div>
        
        <!-- Other file types -->
        <div *ngIf="!isPdf && !isImage" class="flex items-center justify-center">
          <span class="material-icons text-gray-500 text-4xl">insert_drive_file</span>
        </div>
      </div>
      
      <!-- File details section -->
      <div class="p-3">
        <div class="flex items-center justify-between">
          <div class="truncate flex-1">
            <p class="text-sm font-medium truncate" [title]="fileWithMetadata.name">{{ fileWithMetadata.name }}</p>
            <p class="text-xs text-gray-500">{{ formatSize(fileWithMetadata.size) }}</p>
          </div>
          <button 
            type="button" 
            (click)="onRemove()" 
            class="ml-2 p-1 text-gray-400 hover:text-red-500 rounded-full hover:bg-gray-100"
            title="Remove file"
          >
            <span class="material-icons">close</span>
          </button>
        </div>
      </div>
    </div>
  `
})
export class FileItemComponent implements OnInit {
  @Input() fileWithMetadata!: FileWithMetadata;
  @Output() remove = new EventEmitter<FileWithMetadata>();
  
  imagePreviewUrl: string | null = null;
  
  get isPdf(): boolean {
    return typeof this.fileWithMetadata.type === 'string' && this.fileWithMetadata.type.includes('pdf') || 
           (typeof this.fileWithMetadata.name === 'string' && this.fileWithMetadata.name.toLowerCase().endsWith('.pdf'));
  }
  
  get isImage(): boolean {
    return typeof this.fileWithMetadata.type === 'string' && this.fileWithMetadata.type.includes('image') || 
           (typeof this.fileWithMetadata.name === 'string' && /\.(jpg|jpeg|png|gif|bmp|webp)$/i.test(this.fileWithMetadata.name));
  }
  
  ngOnInit() {
    this.createImagePreview();
  }
  
  createImagePreview() {
    if (this.isImage && this.fileWithMetadata.file instanceof File) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.imagePreviewUrl = e.target.result;
      };
      reader.readAsDataURL(this.fileWithMetadata.file);
    }
  }

  formatSize(size: number): string {
    if (!size) return 'Unknown size';
    return `${(size / 1024).toFixed(2)} KB`;
  }
  
  onRemove(): void {
    this.remove.emit(this.fileWithMetadata);
  }
}