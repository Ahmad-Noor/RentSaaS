import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-mail-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="divide-y">
      @for (message of messages; track message.id) {
        <div 
          class="flex items-center gap-4 px-4 py-3 hover:bg-gray-50 cursor-pointer"
          [class.bg-blue-50]="message.unread"
        >
          <!-- Selection & Star -->
          <div class="flex items-center gap-2">
            <input type="checkbox" class="rounded border-gray-300">
            <button class="text-gray-400 hover:text-yellow-500">
              <span class="material-icons">star_border</span>
            </button>
          </div>

          <!-- Sender -->
          <div class="w-48">
            <span [class.font-semibold]="message.unread">{{ message.sender }}</span>
          </div>

          <!-- Content -->
          <div class="flex-1 min-w-0">
            <div class="flex items-baseline gap-2">
              <span [class.font-semibold]="message.unread">{{ message.subject }}</span>
              <span class="text-gray-500 truncate">- {{ message.preview }}</span>
            </div>
          </div>

          <!-- Date -->
          <div class="text-sm text-gray-500">
            {{ message.date }}
          </div>
        </div>
      }
    </div>
  `
})
export class MailListComponent {
  messages = [
    {
      id: 1,
      sender: 'John Smith',
      subject: 'Maintenance Request Update',
      preview: 'The plumber has completed the repairs...',
      date: '5 mins ago',
      unread: true
    },
    {
      id: 2,
      sender: 'Alice Johnson',
      subject: 'Rent Payment Confirmation',
      preview: 'This email confirms your rent payment...',
      date: '2 hours ago',
      unread: true
    },
    {
      id: 3,
      sender: 'Property Management',
      subject: 'Monthly Newsletter',
      preview: 'Here are the updates for this month...',
      date: '1 day ago',
      unread: false
    }
  ];
}