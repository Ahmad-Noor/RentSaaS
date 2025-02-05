import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MailSidebarComponent } from './mail-sidebar/mail-sidebar.component';
import { MailListComponent } from './mail-list/mail-list.component';
import { MailToolbarComponent } from './mail-toolbar/mail-toolbar.component';

@Component({
  selector: 'app-mailbox',
  standalone: true,
  imports: [CommonModule, MailSidebarComponent, MailListComponent, MailToolbarComponent],
  template: `
    <div class="flex h-[calc(100vh-64px)]">
      <!-- Mail Sidebar -->
      <app-mail-sidebar class="w-64 border-r bg-white" />

      <!-- Main Content -->
      <div class="flex-1 flex flex-col bg-gray-50">
        <app-mail-toolbar />
        <app-mail-list class="flex-1 overflow-auto" />
      </div>
    </div>
  `
})
export class MailboxComponent {}