import { Component } from '@angular/core';
import { AZURE_COLORS } from '../../../../../shared/constants/colors';

@Component({
  selector: 'app-navbar-search',
  standalone: true,
  template: `
    <div class="relative">
      <span class="material-icons absolute left-3 top-2 text-gray-400">search</span>
      <input 
        type="text"
        placeholder="Search resources"
        class="w-64 bg-[#1B3A5C] text-white pl-10 pr-4 py-1 rounded border border-[#2B4D6F] focus:outline-none focus:border-[#0078D4]"
      >
    </div>
  `
})
export class NavbarSearchComponent {
  colors = AZURE_COLORS;
}