import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [RouterLink],
  template: `
    <header class="bg-white shadow-sm">
      <nav class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div class="flex justify-between h-16">
          <div class="flex">
            <div class="flex-shrink-0 flex items-center">
              <img class="h-8 w-auto" src="assets/logo.svg" alt="Logo">
            </div>
            <div class="hidden sm:ml-6 sm:flex sm:space-x-8">
              <a routerLink="/" class="text-gray-900 inline-flex items-center px-1 pt-1 border-b-2 border-transparent">
                Home
              </a>
              <a routerLink="/pricing" class="text-gray-500 hover:text-gray-900 inline-flex items-center px-1 pt-1 border-b-2 border-transparent">
                Price
              </a>
              <a routerLink="/about" class="text-gray-500 hover:text-gray-900 inline-flex items-center px-1 pt-1 border-b-2 border-transparent">
                About
              </a>
              <a routerLink="/contact" class="text-gray-500 hover:text-gray-900 inline-flex items-center px-1 pt-1 border-b-2 border-transparent">
                Contact us
              </a>
            </div>
          </div>
          <div class="flex items-center">
            <a routerLink="/login" class="text-gray-500 hover:text-gray-900 px-3 py-2">
              Login
            </a>
            <a routerLink="/register" class="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700">
              Register
            </a>
          </div>
        </div>
      </nav>
    </header>

    <main>
      <!-- Hero Section -->
      <div class="bg-yellow-400 py-16">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div class="text-center">
            <h1 class="text-4xl font-bold text-gray-900 sm:text-5xl md:text-6xl">
              Property Management Made Easy
            </h1>
            <p class="mt-3 max-w-md mx-auto text-base text-gray-800 sm:text-lg md:mt-5 md:text-xl md:max-w-3xl">
              Streamline your property management with our comprehensive solution
            </p>
            <div class="mt-5 max-w-md mx-auto sm:flex sm:justify-center md:mt-8">
              <div class="rounded-md shadow">
                <a routerLink="/register" class="w-full flex items-center justify-center px-8 py-3 border border-transparent text-base font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700 md:py-4 md:text-lg md:px-10">
                  Get started
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Features Section -->
      <div class="py-12 bg-white">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div class="grid grid-cols-1 gap-8 sm:grid-cols-2 lg:grid-cols-3">
            <!-- Feature cards will go here -->
          </div>
        </div>
      </div>
    </main>

    <footer class="bg-gray-800">
      <div class="max-w-7xl mx-auto py-12 px-4 sm:px-6 lg:px-8">
        <div class="text-center text-gray-400">
          <p>&copy; 2024 Property Manager. All rights reserved.</p>
        </div>
      </div>
    </footer>
  `
})
export class MainPage {}