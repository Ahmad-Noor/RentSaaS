import { Component } from '@angular/core';
import { HeaderComponent } from '../../../components/header/header.component';
import { FooterComponent } from '../../../components/footer/footer.component';

@Component({
    selector: 'app-auth-layout',
    imports: [HeaderComponent, FooterComponent],
    template: `
    <div class="min-h-screen flex flex-col">
      <app-header />
      
      <main class="flex-grow bg-gray-50 flex flex-col justify-center py-12 sm:px-6 lg:px-8">
        <ng-content></ng-content>
      </main>

      <app-footer />
    </div>
  `
})
export class AuthLayoutComponent {}