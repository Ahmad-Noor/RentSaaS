import { Component } from '@angular/core';
import { AZURE_COLORS } from '../../../../../../shared/constants/colors';

@Component({
  selector: 'app-navbar-actions',
  standalone: true,
  template: `
    <div class="flex items-center gap-2">
      <button class="p-1 hover:bg-[#0078D4] rounded transition-colors">
        <span class="material-icons">notifications</span>
      </button>
      <button class="p-1 hover:bg-[#0078D4] rounded transition-colors">
        <span class="material-icons">settings</span>
      </button>
      <button class="p-1 hover:bg-[#0078D4] rounded transition-colors">
        <span class="material-icons">help</span>
      </button>
      <button class="p-1 hover:bg-[#0078D4] rounded transition-colors">
        <span class="material-icons">account_circle</span>
      </button>
    </div>
  `
})
export class NavbarActionsComponent {
  colors = AZURE_COLORS;
}