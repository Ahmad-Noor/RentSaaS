import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';   
import { AppRoutingModule } from './app-routing.module'; 
import { DashboardModule } from './modules/dashboard/dashboard.module';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http'; 
import { HomeModule } from './modules/home/home.module';

@NgModule({
  declarations: [
    AppComponent,   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule, 
    DashboardModule,
    HomeModule
  ],
  providers: [
    provideClientHydration(), provideHttpClient(withFetch()), 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
