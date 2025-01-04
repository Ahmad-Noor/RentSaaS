import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'app-mobile-menu',
    imports: [CommonModule, RouterLink],
    template: `
    <div 
      class="fixed inset-0 bg-gray-800 bg-opacity-75 z-50 lg:hidden"
      [class.hidden]="!isOpen"
      (click)="onClose.emit()"
    >
      <div 
        class="fixed inset-y-0 right-0 max-w-xs w-full bg-white shadow-xl p-6"
        (click)="$event.stopPropagation()"
      >
        <div class="flex items-center justify-between mb-8">
          <h2 class="text-xl font-semibold">Menu</h2>
          <button 
            class="text-gray-600 hover:text-gray-900"
            (click)="onClose.emit()"
          >
            <span class="material-icons">close</span>
          </button>
        </div>

        <nav class="space-y-6">
          <a 
            *ngFor="let link of links"
            [routerLink]="link.path"
            class="block text-gray-600 hover:text-gray-900"
            (click)="onClose.emit()"
          >
            {{ link.label }}
          </a>

          <div class="pt-6 border-t">
            <a 
              routerLink="/login"
              class="block w-full text-center bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 mb-3"
              (click)="onClose.emit()"
            >
              Log In
            </a>
            <a 
              routerLink="/register"
              class="block w-full text-center border border-blue-600 text-blue-600 px-4 py-2 rounded-md hover:bg-blue-50"
              (click)="onClose.emit()"
            >
              Sign Up
            </a>
          </div>
        </nav>
      </div>
    </div>
  `
})
export class MobileMenuComponent {
  @Input() isOpen = false;
  @Output() onClose = new EventEmitter<void>();

  links = [
    { path: '/features', label: 'Features' },
    { path: '/pricing', label: 'Pricing' },
    { path: '/about', label: 'About' },
    { path: '/contact', label: 'Contact' }
  ];
}