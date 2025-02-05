import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  standalone: true,
  template: `
    <footer class="bg-[#1B3A5C] text-white py-16 px-8">
      <div class="max-w-6xl mx-auto grid grid-cols-4 gap-8">
        <div>
          <h3 class="font-bold mb-4">RESOURCES</h3>
          <ul class="space-y-2">
            <li><a href="#">Support</a></li>
            <li><a href="#">Training</a></li>
            <li><a href="#">Documentation</a></li>
          </ul>
        </div>
        <div>
          <h3 class="font-bold mb-4">ABOUT</h3>
          <ul class="space-y-2">
            <li><a href="#">Our Company</a></li>
            <li><a href="#">Careers</a></li>
            <li><a href="#">Contact Us</a></li>
          </ul>
        </div>
        <div>
          <h3 class="font-bold mb-4">CONNECT</h3>
          <div class="flex gap-4">
            <a href="#" class="hover:text-blue-400">Facebook</a>
            <a href="#" class="hover:text-blue-400">Twitter</a>
            <a href="#" class="hover:text-blue-400">LinkedIn</a>
          </div>
        </div>
        <div>
          <h3 class="font-bold mb-4">JOIN OUR MAILING LIST</h3>
          <input type="email" placeholder="Email" class="w-full p-2 rounded text-black mb-2">
          <button class="bg-orange-500 text-white px-4 py-2 rounded w-full">Sign Up</button>
        </div>
      </div>
    </footer>
  `
})
export class FooterComponent {}