import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';  
import { Receipt } from '../../../../models/receipt.types';
import { validateReceipt } from '../../../../utils/receipt.utils';
import { ReceiptItemComponent } from './receipt-item.component';

@Component({
  selector: 'app-receipt-upload',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ReceiptItemComponent],
  template: `
    <div [formGroup]="formGroup" class="space-y-4">
      <div class="flex items-center gap-2">
        <button
          type="button"
          (click)="fileInput.click()"
          class="flex items-center gap-2 px-4 py-2 text-blue-600 bg-blue-50 rounded-lg hover:bg-blue-100"
        >
          <span class="material-icons">upload_file</span>
          Upload Receipts
        </button>
        <span class="text-sm text-gray-500">Upload up to 5 receipts</span>
      </div>
      
      <input
        #fileInput
        type="file"
        (change)="onFilesSelected($event)"
        accept="image/*,.pdf"
        class="hidden"
        multiple
      >
      
      @if (error) {
        <p class="text-sm text-red-600">{{ error }}</p>
      }

      @if (receipts.length > 0) {
        <div class="space-y-2">
          @for (receipt of receipts; track receipt.id) {
            <app-receipt-item
              [receipt]="receipt"
              (onRemove)="removeReceipt($event)"
            />
          }
        </div>
      }
    </div>
  `
})
export class ReceiptUploadComponent {
  @Input() formGroup!: FormGroup;
  receipts: Receipt[] = [];
  error = '';

  onFilesSelected(event: Event): void {
    const files = Array.from((event.target as HTMLInputElement).files || []);
    
    if (this.receipts.length + files.length > 5) {
      this.error = 'You can upload a maximum of 5 receipts';
      return;
    }

    files.forEach(file => {
      const validation = validateReceipt(file);
      if (!validation.isValid) {
        this.error = validation.error || 'Invalid file';
        return;
      }

      const receipt: Receipt = {
        id: crypto.randomUUID(),
        file,
        name: file.name,
        size: file.size,
        type: file.type
      };

      this.receipts.push(receipt);
    });

    this.formGroup.patchValue({ receipts: this.receipts });
    (event.target as HTMLInputElement).value = '';
  }

  removeReceipt(receipt: Receipt): void {
    this.receipts = this.receipts.filter(r => r.id !== receipt.id);
    this.formGroup.patchValue({ receipts: this.receipts });
    this.error = '';
  }
}