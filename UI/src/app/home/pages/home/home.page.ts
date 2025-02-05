import { Component, Inject, PLATFORM_ID } from "@angular/core";
import { CommonModule, isPlatformServer } from "@angular/common";
import { HeaderComponent } from "../../../shared/header/header.component";
import { FeatureTabsComponent } from "../../components/feature-tabs/feature-tabs.component";
import { FormsSectionComponent } from "../../components/forms/forms-section.component";
import { FooterComponent } from "../../../shared/footer/footer.component";
import { HomeSectionComponent } from "../../components/home-section/home-section.component";

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
