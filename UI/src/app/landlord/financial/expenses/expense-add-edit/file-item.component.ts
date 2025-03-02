import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { FileWithMetadata } from '../../../../models/fileWithMetadata.types';

@Component({
  selector: 'app-file-item',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="bg-white rounded-lg shadow-md overflow-hidden border border-gray-200 hover:shadow-lg transition-shadow">
      <div class="flex items-center p-3">
        <!-- Left side preview - small thumbnail -->
        <div class="w-16 h-16 flex-shrink-0 mr-3 flex items-center justify-center bg-gray-100 rounded overflow-hidden">
          <!-- PDF Preview -->
          <div *ngIf="isPdf" class="flex flex-col items-center justify-center h-full w-full">
            <span class="material-icons text-red-500">picture_as_pdf</span>
          </div>
          
          <!-- Image Preview -->
          <div *ngIf="isImage" class="h-full w-full">
            <img 
              *ngIf="imagePreviewUrl" 
              [src]="imagePreviewUrl" 
              alt="Preview" 
              class="h-full w-full object-cover"
            >
            <div *ngIf="!imagePreviewUrl" class="flex items-center justify-center h-full">
              <span class="material-icons text-blue-500">image</span>
            </div>
          </div>
          
          <!-- Other file types -->
          <div *ngIf="!isPdf && !isImage" class="flex items-center justify-center h-full w-full">
            <span class="material-icons text-gray-500">insert_drive_file</span>
          </div>
        </div>
        
        <!-- Right side file details -->
        <div class="flex-1 min-w-0">
          <div class="flex items-center justify-between mb-1">
            <div class="truncate">
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
          
          <!-- View button -->
          <div class="mt-1">
            <button 
              type="button"
              (click)="openFilePreview()"
              class="text-xs bg-blue-50 text-blue-600 px-2 py-1 rounded hover:bg-blue-100 flex items-center"
            >
              <span class="material-icons text-sm mr-1">visibility</span>
              View {{ isPdf ? 'PDF' : (isImage ? 'Image' : 'File') }}
            </button>
          </div>
        </div>
      </div>
    </div>

    <!-- File Preview Modal -->
    <div *ngIf="showFilePreview" class="fixed inset-0 bg-black bg-opacity-50 z-50 flex items-center justify-center p-4">
      <div class="bg-white rounded-lg shadow-xl w-full max-w-4xl h-[80vh] flex flex-col">
        <div class="flex items-center justify-between p-4 border-b">
          <h3 class="text-lg font-medium">{{ fileWithMetadata.name }}</h3>
          <button 
            type="button" 
            (click)="closeFilePreview()"
            class="text-gray-400 hover:text-gray-600"
          >
            <span class="material-icons">close</span>
          </button>
        </div>
        <div class="flex-1 overflow-auto flex items-center justify-center bg-gray-50">
          <!-- PDF Preview -->
          <iframe 
            *ngIf="isPdf && pdfPreviewUrl" 
            [src]="pdfPreviewUrl" 
            class="w-full h-full" 
            type="application/pdf"
          ></iframe>
          
          <!-- Image Preview -->
          <div *ngIf="isImage && imageModalUrl" class="h-full w-full flex items-center justify-center p-4">
            <img 
              [src]="imageModalUrl" 
              alt="Full preview" 
              class="max-h-full max-w-full object-contain"
            >
          </div>
        </div>
      </div>
    </div>
  `
})
export class FileItemComponent implements OnInit {
  @Input() fileWithMetadata!: FileWithMetadata;
  @Output() remove = new EventEmitter<FileWithMetadata>();
  
  imagePreviewUrl: string | null = null;
  imageModalUrl: string | null = null;
  pdfPreviewUrl: SafeResourceUrl | null = null;
  showFilePreview = false;
  
  constructor(private sanitizer: DomSanitizer) {}
  
  get isPdf(): boolean {
    return typeof this.fileWithMetadata.type === 'string' && this.fileWithMetadata.type.includes('pdf') || 
           (typeof this.fileWithMetadata.name === 'string' && this.fileWithMetadata.name.toLowerCase().endsWith('.pdf'));
  }
  
  get isImage(): boolean {
    return typeof this.fileWithMetadata.type === 'string' && this.fileWithMetadata.type.includes('image') || 
           (typeof this.fileWithMetadata.name === 'string' && /\.(jpg|jpeg|png|gif|bmp|webp)$/i.test(this.fileWithMetadata.name));
  }
  ngOnInit() {
    // First check if we have a URL (for files from server)
    if (this.fileWithMetadata.url) {
      if (this.isImage) {
        this.imagePreviewUrl = this.fileWithMetadata.url;
      }
    } else {
      // Only try to create preview from file if there's no URL
      this.createImagePreview();
    }
  }
  
  createImagePreview() {
    if (this.isImage && this.fileWithMetadata.file instanceof File && this.fileWithMetadata.file.size > 0) {
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
  
  openFilePreview(): void {
    if (this.isPdf) {
      this.openPdfPreview();
    } else if (this.isImage) {
      this.openImagePreview();
    }
  }
  
  openPdfPreview(): void {
    // First check if we have a URL (for files from server)
    if (this.fileWithMetadata.url) {
      this.pdfPreviewUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.fileWithMetadata.url);
      this.showFilePreview = true;
    } 
    // Then try to use the file if available and not empty
    else if (this.fileWithMetadata.file instanceof File && this.fileWithMetadata.file.size > 0) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.pdfPreviewUrl = this.sanitizer.bypassSecurityTrustResourceUrl(e.target.result);
        this.showFilePreview = true;
      };
      reader.readAsDataURL(this.fileWithMetadata.file);
    } else {
      console.error('Cannot preview PDF: No valid file or URL available');
      alert('Cannot preview this PDF. Try downloading it instead.');
    }
  }
  
  openImagePreview(): void {
    // Use URL if available
    if (this.fileWithMetadata.url) {
      this.imageModalUrl = this.fileWithMetadata.url;
      this.showFilePreview = true;
    }
    // Use cached preview if available 
    else if (this.imagePreviewUrl) {
      this.imageModalUrl = this.imagePreviewUrl;
      this.showFilePreview = true;
    }
    // Try to read from file if available and not empty
    else if (this.fileWithMetadata.file instanceof File && this.fileWithMetadata.file.size > 0) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.imageModalUrl = e.target.result;
        this.imagePreviewUrl = e.target.result;
        this.showFilePreview = true;
      };
      reader.readAsDataURL(this.fileWithMetadata.file);
    } else {
      console.error('Cannot preview image: No valid file or URL available');
      alert('Cannot preview this image. Try downloading it instead.');
    }
  }
  
  closeFilePreview(): void {
    this.showFilePreview = false;
  }
}