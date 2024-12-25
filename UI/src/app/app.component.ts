import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
// import { HomeComponent } from './Pages/home/home.component';
import { NavbarComponent } from './Shaerd/navbar/navbar.component';
import { FooterComponent } from './Shaerd/footer/footer.component';
@Component({
  selector: 'app-root',
  imports: [
 RouterModule,
    // HomeComponent,
    NavbarComponent,
    FooterComponent,
 
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'RentSaaSUI';
}
