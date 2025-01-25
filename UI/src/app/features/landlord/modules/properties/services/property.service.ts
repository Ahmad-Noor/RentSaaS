import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Property, PropertyCreate } from '../types/property.types';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UUID } from 'crypto';

@Injectable({
  providedIn: "root",
})
export class PropertyService {
  private properties!: BehaviorSubject<Property[]>;

  private headers = new HttpHeaders({
    "Content-Type": "application/json",
    "X-OrganizationId": "00000000-0000-0000-0000-000000000001",
    Authorization:
      "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI5OGE5YzRhMC1hNzFiLTQ4MDItYWFmNi03MTk5MDAyMTkxMjIiLCJzdWIiOiJIYXJlZHlzc2FAcmVudHNhYXMuY29tIiwiZW1haWwiOiJIYXJlZHlzc2FAcmVudHNhYXMuY29tIiwiZ2l2ZW5fbmFtZSI6Ik1vaGFtZWRzYSBIYXJlZHlzYXMiLCJqdGkiOiIxY2YwN2VhZi1kMjg0LTQ0MWMtYjU0OS05Yzk3ZDE1NWRlZjYiLCJuYmYiOjE3Mzc1NzIzMjEsImV4cCI6MTczNzY1ODcyMSwiaWF0IjoxNzM3NTcyMzIxfQ.QpE-A3SlB4Zs8x4GrYvi--GHbJ190whKZY4nMEiUjK4",
  });

  constructor(private _httpClient: HttpClient) {
    this.properties = new BehaviorSubject<Property[]>([]);
  }

  getAllProperties(): Observable<any> {
    return this._httpClient.get("https://localhost:44327/api/Property/GetAll", {
      headers: this.headers,
    });
  }

  getProperties(): Observable<any> {
    return this.properties.asObservable();
  }

  CreateNewProperty(Property: PropertyCreate): Observable<any> {
    console.log("Final PropertyDataAll:", Property);

    console.log(JSON.stringify(Property)); // Log the payload
    return this._httpClient.post(
      "https://localhost:44327/api/Property/Add",
      Property, // Use the extended object here
      { headers: this.headers }
    );
  }




Delete(id:UUID):Observable<any>
{

  return this._httpClient.delete(`https://localhost:44327/api/Property/${id}`,{ headers: this.headers })
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