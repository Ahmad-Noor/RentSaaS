import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabNavComponent } from './components/tab-nav.component';
import { TabContentComponent } from './components/tab-content.component';
import { FEATURE_TABS } from './feature-tabs.data';

@Component({
    selector: 'app-feature-tabs',
    imports: [CommonModule, TabNavComponent, TabContentComponent],
    template: `
    <section class="bg-[#F8FBFF] py-16">
      <div class="max-w-7xl mx-auto px-4">
        <!-- Tab Navigation -->
        <app-tab-nav
          [tabs]="tabs"
          [activeTabId]="activeTabId"
          (onTabSelect)="selectTab($event)"
          class="mb-8 flex justify-center"
        />

        <!-- Tab Content -->
        @if (activeTab) {
          <app-tab-content [content]="activeTab.content" />
        }
      </div>
    </section>
  `,
    styles: [`
    :host {
      display: block;
    }
  `]
})
export class FeatureTabsComponent {
  tabs = FEATURE_TABS;
  activeTabId = this.tabs[0].id;

  get activeTab() {
    return this.tabs.find(tab => tab.id === this.activeTabId);
  }

  selectTab(id: string): void {
    this.activeTabId = id;
  }
}