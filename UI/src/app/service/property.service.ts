import { UUID } from 'crypto';  
import { Observable } from 'rxjs'; 
import { Constant } from '../constants';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Property } from '../models/property.model';

@Injectable({
  providedIn: "root",
})
export class PropertyService {

  apiUrl: string = environment.apiUrl + 'api/property';
  headers : HttpHeaders=new HttpHeaders({
    "X-OrganizationId": `${localStorage.getItem(Constant.OrganizationIdRentSass)}`,
    "Authorization": `Bearer ${localStorage.getItem(Constant.token)}`,
  }); 

  constructor(private http: HttpClient) { }
  
  getAllProperties(): Observable<Property[]> {
    return this.http.get<Property[]>(`${this.apiUrl}/GetAll`, {headers: this.headers,});
  }

  getPropertyById(id: number): Observable<Property> {
    return this.http.get<Property>(`${this.apiUrl}/${id}`,{ headers: this.headers });
  }

  addProperty(data: Property): Observable<Property> {
    return this.http.post<Property>(`${this.apiUrl}/Add`, data,{ headers: this.headers });
  }

  updateProperty(id: string, data: Property): Observable<Property> {
    return this.http.put<Property>(this.apiUrl, data,{ headers: this.headers });
  }

  deleteProperty(id: string): Observable<Property> {
    return this.http.delete<Property>(`${this.apiUrl}/${id}`,{ headers: this.headers });
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
  