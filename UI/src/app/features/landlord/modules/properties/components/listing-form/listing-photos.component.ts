import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ListingPhoto } from '../../types/listing.types';

@Component({
  selector: 'app-listing-photos',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div [formGroup]="formGroup" class="space-y-4">
      <label class="block text-sm font-medium text-gray-700">Property Photos</label>
      
      <div class="flex items-center gap-2">
        <button
          type="button"
          (click)="fileInput.click()"
          class="flex items-center gap-2 px-4 py-2 text-blue-600 bg-blue-50 rounded-lg hover:bg-blue-100"
        >
          <span class="material-icons">photo_camera</span>
          Add Photos
        </button>
        <span class="text-sm text-gray-500">Upload up to 20 photos</span>
      </div>
      
      <input
        #fileInput
        type="file"
        (change)="onFilesSelected($event)"
        accept="image/*"
        class="hidden"
        multiple
      >

      @if (photos.length > 0) {
        <div class="grid grid-cols-2 md:grid-cols-4 lg:grid-cols-6 gap-4">
          @for (photo of photos; track photo.id) {
            <div class="relative group">
              <img 
                [src]="getPhotoUrl(photo)"
                [alt]="photo.name"
                class="w-full h-24 object-cover rounded"
              >
              <button
                type="button"
                (click)="removePhoto(photo)"
                class="absolute top-2 right-2 p-1 bg-white rounded-full shadow opacity-0 group-hover:opacity-100 transition-opacity"
              >
                <span class="material-icons text-red-500">close</span>
              </button>
            </div>
          }
        </div>
      }
    </div>
  `
})
export class ListingPhotosComponent {
  @Input() formGroup!: FormGroup;
  photos: ListingPhoto[] = [];

  onFilesSelected(event: Event): void {
    const files = Array.from((event.target as HTMLInputElement).files || []);
    
    files.forEach(file => {
      const photo: ListingPhoto = {
        id: crypto.randomUUID(),
        file,
        name: file.name
      };
      this.photos.push(photo);
    });

    this.formGroup.patchValue({ photos: this.photos });
    (event.target as HTMLInputElement).value = '';
  }

  getPhotoUrl(photo: ListingPhoto): string {
    return URL.createObjectURL(photo.file);
  }

  removePhoto(photo: ListingPhoto): void {
    this.photos = this.photos.filter(p => p.id !== photo.id);
    this.formGroup.patchValue({ photos: this.photos });
  }
}