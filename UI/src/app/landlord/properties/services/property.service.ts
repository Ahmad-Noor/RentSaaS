import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Property, PropertyCreate } from '../types/property.types';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UUID } from 'crypto';
import { environment } from '../../../../environments/environment';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: "root",
})
export class PropertyService {

  baseUrl = environment.apiUrl;

  private properties!: BehaviorSubject<Property[]>;
  private headers!: HttpHeaders;

  constructor(private _httpClient: HttpClient, @Inject(PLATFORM_ID) private platformId: Object) {
    this.properties = new BehaviorSubject<Property[]>([]);
    this.initializeHeaders(); // Initialize headers here
  }

  private initializeHeaders(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.headers = new HttpHeaders({
        "X-OrganizationId": `${localStorage.getItem('organizationId')}`,
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      });

      console.log(this.headers);
    } else {
      this.headers = new HttpHeaders(); // Empty headers for non-browser platforms
    }
  }

  getAllProperties(): Observable<any> {
    return this._httpClient.get(`${this.baseUrl}api/Property/GetAll`, {
      headers: this.headers,
    });
  }

  // getProperties(): Observable<any> {
  //   return this.properties.asObservable();
  // }

  CreateNewProperty(Property: PropertyCreate): Observable<any> {
    return this._httpClient.post(
      `${this.baseUrl}api/Property/Add`,
      Property,
      { headers: this.headers }
    );
  }

  Delete(id: UUID): Observable<any> {
    return this._httpClient.delete(`${this.baseUrl}api/Property/Delete/${id}`, { headers: this.headers });
  }

  private getPropertyTypeLabel(type: string): string {
    const labels: Record<string, string> = {
      house: "Single Family",
      condo: "Condo/Apartment",
      townhouse: "Townhouse",
      community: "Multi-family",
    };
    return labels[type] || type;
  }
}
  