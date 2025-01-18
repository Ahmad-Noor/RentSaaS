import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface TableColumn {
  key: string;
  label: string;
  type?: 'text' | 'status' | 'actions';
}

@Component({
    selector: 'app-data-table',
    imports: [CommonModule],
    template: `
    <table class="w-full">
      <thead>
        <tr class="border-b">
          @for (column of columns; track column.key) {
            <th class="text-left py-3 px-4">{{ column.label }}</th>
          }
        </tr>
      </thead>
      <tbody>
        @for (row of data; track row.id) {
          <tr class="border-b hover:bg-gray-50">
            @for (column of columns; track column.key) {
              <td class="py-3 px-4">
                @switch (column.type) {
                  @case ('status') {
                    <span [class]="getStatusClass(row[column.key])">
                      {{ row[column.key] }}
                    </span>
                  }
                  @case ('actions') {
                    <div class="flex gap-2">
                      <button 
                        class="text-gray-600 hover:text-blue-600"
                        (click)="onAction.emit({ type: 'edit', item: row })"
                      >
                        <span class="material-icons">edit</span>
                      </button>
                      <button 
                        class="text-gray-600 hover:text-red-600"
                        (click)="onAction.emit({ type: 'delete', item: row })"
                      >
                        <span class="material-icons">delete</span>
                      </button>
                    </div>
                  }
                  @default {
                    {{ row[column.key] }}
                  }
                }
              </td>
            }
          </tr>
        }
      </tbody>
    </table>
  `
})
export class DataTableComponent {
  @Input() columns!: TableColumn[];
  @Input() data!: any[];
  @Output() onAction = new EventEmitter<{ type: string; item: any }>();

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm';
    const statusClasses: Record<string, string> = {
      'Active': 'bg-green-100 text-green-800',
      'Inactive': 'bg-gray-100 text-gray-800',
      'Pending': 'bg-yellow-100 text-yellow-800',
      'Completed': 'bg-blue-100 text-blue-800'
    };

    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }
}