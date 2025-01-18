import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { PhotoItemComponent } from './photo-item.component';
import { Photo } from '../../types/maintenance.types';
import { validatePhoto } from '../../utils/photo.utils';

@Component({
  selector: 'app-photo-upload',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, PhotoItemComponent],
  template: `
    <div [formGroup]="formGroup" class="space-y-4">
      <div class="flex items-center gap-2">
        <button
          type="button"
          (click)="fileInput.click()"
          class="flex items-center gap-2 px-4 py-2 text-blue-600 bg-blue-50 rounded-lg hover:bg-blue-100"
        >
          <span class="material-icons">photo_camera</span>
          Add Photos
        </button>
        <span class="text-sm text-gray-500">Upload up to 5 photos</span>
      </div>
      
      <input
        #fileInput
        type="file"
        (change)="onFilesSelected($event)"
        accept="image/*"
        class="hidden"
        multiple
      >
      
      @if (error) {
        <p class="text-sm text-red-600">{{ error }}</p>
      }

      @if (photos.length > 0) {
        <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
          @for (photo of photos; track photo.id) {
            <app-photo-item
              [photo]="photo"
              (remove)="removePhoto($event)"
            />
          }
        </div>
      }
    </div>
  `
})
export class PhotoUploadComponent {
  @Input() formGroup!: FormGroup;
  photos: Photo[] = [];
  error = '';

  onFilesSelected(event: Event): void {
    const files = Array.from((event.target as HTMLInputElement).files || []);
    
    if (this.photos.length + files.length > 5) {
      this.error = 'You can upload a maximum of 5 photos';
      return;
    }

    files.forEach(file => {
      const validation = validatePhoto(file);
      if (!validation.isValid) {
        this.error = validation.error || 'Invalid file';
        return;
      }

      const photo: Photo = {
        id: crypto.randomUUID(),
        file,
        name: file.name,
        size: file.size,
        type: file.type
      };

      this.photos.push(photo);
    });

    this.formGroup.patchValue({ photos: this.photos });
    (event.target as HTMLInputElement).value = '';
  }

  removePhoto(photo: Photo): void {
    this.photos = this.photos.filter(p => p.id !== photo.id);
    this.formGroup.patchValue({ photos: this.photos });
    this.error = '';
  }
}