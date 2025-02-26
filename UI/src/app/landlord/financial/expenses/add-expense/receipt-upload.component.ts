import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Receipt } from '../../../../models/receipt.types';
import { ReceiptItemComponent } from './receipt-item.component';
import { validateReceipt } from '../../../../utils/receipt.utils';

@Component({
  selector: 'app-receipt-upload',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ReceiptItemComponent],
  template: `
    <div [formGroup]="formGroup" class="space-y-4">
      <div class="flex items-center gap-2">
        <button type="button" (click)="fileInput.click()"
          class="flex items-center gap-2 px-4 py-2 text-blue-600 bg-blue-50 rounded-lg hover:bg-blue-100">
          <span class="material-icons">upload_file</span>
          Upload Receipts
        </button>
        <span class="text-sm text-gray-500">Upload up to 5 receipts</span>
      </div>
      
      <input #fileInput type="file" (change)="onFilesSelected($event)" accept="image/*,.pdf"
        class="hidden" multiple>
      
      <div *ngIf="error" class="text-sm text-red-600">
        {{ error }}
      </div>
      
      <div *ngIf="receipts.length > 0" class="space-y-2">
        <app-receipt-item *ngFor="let receipt of receipts; trackBy: trackByReceiptId"
          [receipt]="receipt" (onRemove)="removeReceipt($event)">
        </app-receipt-item>
      </div>
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
  
  trackByReceiptId(index: number, receipt: Receipt): string {
    return receipt.id;
  }
}