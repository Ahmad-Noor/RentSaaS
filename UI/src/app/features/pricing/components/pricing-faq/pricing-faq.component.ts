import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-pricing-faq',
    imports: [CommonModule],
    template: `
    <section class="py-12">
      <h2 class="text-2xl font-bold text-center mb-8">Pricing FAQs</h2>
      <div class="max-w-3xl mx-auto space-y-4">
        @for (item of faqItems; track item.question) {
          <div class="bg-white rounded-lg shadow p-4">
            <h3 class="font-semibold mb-2">{{ item.question }}</h3>
            <p class="text-gray-600">{{ item.answer }}</p>
          </div>
        }
      </div>
    </section>
  `
})
export class PricingFaqComponent {
  faqItems = [
    {
      question: 'What features are included in the free plan?',
      answer: 'The free plan includes basic property listing, tenant screening, and maintenance request management.'
    },
    {
      question: 'Can I upgrade or downgrade my plan at any time?',
      answer: 'Yes, you can change your plan at any time. Changes will be reflected in your next billing cycle.'
    },
    {
      question: 'Is there a contract or minimum commitment?',
      answer: 'No, all our plans are month-to-month with no long-term commitment required.'
    }
  ];
}