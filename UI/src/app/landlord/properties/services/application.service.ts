 
import { BehaviorSubject, Observable } from 'rxjs';
import { Application, ApplicationCreate } from '../types/application.types';
import { environment } from '../../../../environments/environment';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
 
import { Property, PropertyCreate } from '../types/property.types';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UUID } from 'crypto';
 
import { isPlatformBrowser } from '@angular/common';
import { Constant } from '../../../constants';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {
     
  baseUrl = environment.apiUrl;



  private applications!: BehaviorSubject<Application[]>;
  private headers!: HttpHeaders;

  constructor(private _httpClient: HttpClient, @Inject(PLATFORM_ID) private platformId: Object) {
    this.applications = new BehaviorSubject<Application[]>([]);
    this.initializeHeaders(); // Initialize headers here
  }

  private initializeHeaders(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.headers = new HttpHeaders({
        "X-OrganizationId": `${localStorage.getItem(Constant.OrganizationIdRentSass)}`,
        Authorization: `Bearer ${localStorage.getItem(Constant.token)}`,
      });

      console.log(this.headers);
    } else {
      this.headers = new HttpHeaders();  
    }
  }

 
 updateApplicationStatus(id: number, status: Application['status']): void {
   const currentApplications = this.applications.getValue();
   const updatedApplications = currentApplications.map(app => 
     app.id === id ? { ...app, status } : app
   );
   this.applications.next(updatedApplications);
 }
 
  deleteApplication(id: number): void {
    const currentApplications = this.applications.getValue();
    this.applications.next(currentApplications.filter(app => app.id !== id));
  }
 
 
  getApplications(): Observable<any> {
    return this._httpClient.get(`${this.baseUrl}api/ApplicationAndLeads`, {
      headers: this.headers,
    });
  }
 
  CreateNewApplication(x: ApplicationCreate): Observable<any> {
    return this._httpClient.post(
      `${this.baseUrl}api/ApplicationAndLeads`,
      x,
      { headers: this.headers }
    );
  }

 // Delete(id: UUID): Observable<any> {
 //   return this._httpClient.delete(`${this.baseUrl}api/Property/Delete/${id}`, { headers: this.headers });
 // }
//
 // private getPropertyTypeLabel(type: string): string {
 //   const labels: Record<string, string> = {
 //     house: "Single Family",
 //     condo: "Condo/Apartment",
 //     townhouse: "Townhouse",
 //     community: "Multi-family",
 //   };
 //   return labels[type] || type;
 // }


}