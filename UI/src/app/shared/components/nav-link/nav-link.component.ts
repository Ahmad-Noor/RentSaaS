import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-nav-link',
  standalone: true,
  template: `
    <a 
      [href]="href" 
      class="hover:text-primary transition-colors"
    >
      {{ label }}
    </a>
  `
})
export class NavLinkComponent {
  @Input() href!: string;
  @Input() label!: string;
}