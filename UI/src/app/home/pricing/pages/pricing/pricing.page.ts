import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HeaderComponent } from "../../../../shared/header/header.component";
import { PricingCardComponent } from "../../components/pricing-card/pricing-card.component";
import { PricingFaqComponent } from "../../components/pricing-faq/pricing-faq.component";

@Component({
  selector: "app-pricing-page",
  imports: [
    CommonModule,
    HeaderComponent,
    PricingCardComponent,
    PricingFaqComponent,
  ],
  standalone:true,
  styleUrl: "./pricing.page.css",
  templateUrl:'./pricing.page.html',
})
export class PricingPage {}
