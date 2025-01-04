import { Component } from '@angular/core';

@Component({
  selector: 'app-webinar-banner',
  standalone: true,
  template: `
    <div class="bg-[#1B3A5C] text-white py-3 px-8 flex justify-center items-center gap-4">
      <span>Upcoming WEBINAR</span>
      <span>December 26, 2023</span>
      <button class="bg-blue-500 px-4 py-1 rounded">Register Now</button>
    </div>
  `
})
export class WebinarBannerComponent {}