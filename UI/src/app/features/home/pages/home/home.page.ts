import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformServer } from '@angular/common';
import { HeaderComponent } from '../../../../components/header/header.component';
import { HeroSectionComponent } from '../../components/hero-section/hero-section.component';
import { FeatureTabsComponent } from '../../components/feature-tabs/feature-tabs.component';
import { FormsSectionComponent } from '../../components/forms/forms-section.component';
import { FooterComponent } from '../../../../components/footer/footer.component';

@Component({
    selector: 'app-home-page',
    imports: [
        CommonModule,
        HeaderComponent,
        HeroSectionComponent,
        FeatureTabsComponent,
        FormsSectionComponent,
        FooterComponent
    ],
    template: `
    <app-header />
    
    <main>
      <app-hero-section />
      <app-feature-tabs />
      <app-forms-section />
    </main>

    <app-footer />
  `
})
export class HomePage {

  /**
   *
   */
  constructor( @Inject(PLATFORM_ID) platformId: Object) {

  }
}