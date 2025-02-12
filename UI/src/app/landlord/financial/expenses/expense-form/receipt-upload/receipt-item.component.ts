import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Receipt } from '../../../types/receipt.types';
 import { CommonModule } from '@angular/common'; 
import { formatFileSize } from '../../../utils/file.utils';

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
          <p class="text-xs text-gray-500">{{ formatFileSize(receipt.size) }}</p>
        </div>
      </div>
      
      <button
        type="button"
        (click)="remove.emit(receipt)"
        class="text-gray-400 hover:text-red-500"
      >
        <span class="material-icons">close</span>
      </button>
    </div>
  `
})
export class ReceiptItemComponent {
  @Input() receipt!: Receipt;
  @Output() remove = new EventEmitter<Receipt>();
  
  protected formatFileSize = formatFileSize;
}