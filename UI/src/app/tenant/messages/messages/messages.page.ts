import { Component } from '@angular/core';

@Component({
  selector: 'app-messages-page',
  standalone: true,
  template: `
    <div class="p-4">
      <h1 class="text-2xl font-bold mb-4">Messages</h1>
      <div class="bg-white rounded-lg shadow">
        <div class="border-b px-4 py-3">
          <h2 class="text-lg font-semibold">Inbox</h2>
        </div>
        <!-- Message list -->
      </div>
    </div>
  `
})
export class MessagesPage {}