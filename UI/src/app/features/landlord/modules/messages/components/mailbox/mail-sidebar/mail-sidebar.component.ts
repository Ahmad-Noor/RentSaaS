import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-mail-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="h-full flex flex-col">
      <!-- Compose Button -->
      <div class="p-4">
        <a 
          routerLink="compose"
          class="flex items-center gap-2 bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition-colors w-full justify-center"
        >
          <span class="material-icons text-sm">edit</span>
          Compose
        </a>
      </div>

      <!-- Folders -->
      <nav class="flex-1">
        <ul class="space-y-1 px-3">
          @for (folder of folders; track folder.id) {
            <li>
              <a 
                [routerLink]="['/landlord/messages', folder.id]"
                routerLinkActive="bg-blue-50 text-blue-600"
                class="flex items-center gap-3 px-3 py-2 rounded-lg hover:bg-gray-50 transition-colors"
              >
                <span class="material-icons">{{ folder.icon }}</span>
                <span>{{ folder.label }}</span>
                @if (folder.count) {
                  <span class="ml-auto text-sm bg-gray-100 px-2 py-0.5 rounded-full">
                    {{ folder.count }}
                  </span>
                }
              </a>
            </li>
          }
        </ul>
      </nav>
    </div>
  `
})
export class MailSidebarComponent {
  folders = [
    { id: 'inbox', label: 'Inbox', icon: 'inbox', count: 4 },
    { id: 'sent', label: 'Sent', icon: 'send' },
    { id: 'drafts', label: 'Drafts', icon: 'drafts', count: 2 },
    { id: 'trash', label: 'Trash', icon: 'delete' }
  ];
}