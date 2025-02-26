import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Receipt } from '../../../../models/receipt.types';

@Component({
  selector: 'app-receipt-item',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="flex items-center justify-between p-2 border rounded">
      <div class="flex items-center gap-2">
        <span class="material-icons text-gray-400">
          {{ receipt.type.includes('pdf') ? 'picture_as_pdf' : 'image' }}
        </span>
        <div>
          <p class="text-sm font-medium">{{ receipt.name }}</p>
          <p class="text-xs text-gray-500">{{ formatSize(receipt.size) }}</p>
        </div>
      </div>
      <button type="button" (click)="onRemove.emit(receipt)" class="text-gray-400 hover:text-red-500">
        <span class="material-icons">close</span>
      </button>
    </div>
  `
})
export class ReceiptItemComponent {
  @Input() receipt!: Receipt;
  @Output() onRemove = new EventEmitter<Receipt>();

  formatSize(size: number): string {
    return `${(size / 1024).toFixed(2)} KB`;
  }
}