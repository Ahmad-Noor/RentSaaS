import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-social-login',
  standalone: true,
  template: `
    <div class="space-y-3">
      <button
        (click)="loginWithGoogle()"
        class="w-full flex items-center justify-center gap-2 bg-white border border-gray-300 rounded-md py-2 px-4 hover:bg-gray-50"
      >
        <img src="/assets/icons/google.svg" alt="Google" class="w-5 h-5" />
        Log in with Google
      </button>

      <button
        (click)="loginWithFacebook()"
        class="w-full flex items-center justify-center gap-2 bg-[#1877F2] text-white rounded-md py-2 px-4 hover:bg-[#1874E8]"
      >
        <img src="/assets/icons/facebook.svg" alt="Facebook" class="w-5 h-5 brightness-0 invert" />
        Log in with Facebook
      </button>

      <button
        (click)="loginWithApple()"
        class="w-full flex items-center justify-center gap-2 bg-black text-white rounded-md py-2 px-4 hover:bg-gray-900"
      >
        <img src="/assets/icons/apple.svg" alt="Apple" class="w-5 h-5 brightness-0 invert" />
        Log in with Apple
      </button>
    </div>
  `
})
export class SocialLoginComponent {
  constructor(private authService: AuthService) {}

  loginWithGoogle(): void {
    this.authService.loginWithGoogle().subscribe();
  }

  loginWithFacebook(): void {
    this.authService.loginWithFacebook().subscribe();
  }

  loginWithApple(): void {
    this.authService.loginWithApple().subscribe();
  }
}