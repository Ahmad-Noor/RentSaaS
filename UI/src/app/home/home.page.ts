import { Component, Inject, PLATFORM_ID } from "@angular/core";
import { CommonModule, isPlatformServer } from "@angular/common"; 
import { HeaderComponent } from "../shared/header/header.component";
import { HomeSectionComponent } from "./home-section/home-section.component";
import { FeatureTabsComponent } from "./feature-tabs/feature-tabs.component";
import { FormsSectionComponent } from "./forms/forms-section.component";
import { FooterComponent } from "../shared/footer/footer.component";

@Component({
  selector: "app-home-page",
  imports: [
    CommonModule,
    HeaderComponent,
    HomeSectionComponent,
    FeatureTabsComponent,
    FormsSectionComponent,
    FooterComponent,
  ],
  template: `
    <app-header />

    <main>
      <app-home-section />
      <app-feature-tabs />
      <app-forms-section />
    </main>

    <app-footer />
  `,
})
export class HomePage {
  constructor(@Inject(PLATFORM_ID) platformId: Object) {}
}
