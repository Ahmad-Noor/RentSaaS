import { Component } from '@angular/core';  
import { MailboxComponent } from './mailbox/mailbox.component';

@Component({
  selector: 'app-messages-page',
  standalone: true,
  imports: [MailboxComponent],
  template: `<app-mailbox />`
})
export class MessagesPage {}