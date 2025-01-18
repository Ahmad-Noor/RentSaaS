import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Property, PropertyCreate } from '../types/property.types';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  private properties!: BehaviorSubject<Property[]>;

  private headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'X-OrganizationId': '00000000-0000-0000-0000-000000000001',
    'Authorization': 'Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjE0ZDNhZjQ5LTRjY2MtNDVkZi1iMWQyLWZiMDg1MTcwODc5MCIsInN1YiI6IkhhcmVkeXNzQHJlbnRzYWFzLmNvbSIsImVtYWlsIjoiSGFyZWR5c3NAcmVudHNhYXMuY29tIiwiZ2l2ZW5fbmFtZSI6Ik1vaGFtZWRzIEhhcmVkeXNzIiwianRpIjoiODEwNzEyNjgtNzUxZC00ZDdkLThmMDktNDk0MDI4YmZlYWI5IiwibmJmIjoxNzM2MjYxNDg0LCJleHAiOjE3MzYyNjE0OTQsImlhdCI6MTczNjI2MTQ4NH0.mRwD576CkXmCYnu3sK0b4shujpjXIGGcmqas1MjwjBRo0mdb6ZbSpONsZpfyiP7c3FRv9i1unlQ0sEU0OycQ3w'
  });


constructor(private _httpClient: HttpClient) {
  this.properties = new BehaviorSubject<Property[]>([]);
}

getAllProperties():Observable<any> {


  return this._httpClient.get(
    "https://localhost:7164/api/Property", 
    { headers: this.headers }
  );
}

  getProperties(): Observable<any> {
    return this.properties.asObservable();
  }


  CreateNewProperty(Property: PropertyCreate): Observable<any> {

    
    console.log("Final PropertyDataAll:", Property);
    



    console.log(JSON.stringify(Property));  // Log the payload
    return this._httpClient.post(
      "https://localhost:7164/api/Property/Add", 
      Property,  // Use the extended object here
      { headers: this.headers }
    );
}

  // addProperty(propertyData: CreatePropertyDTO): Observable<Property> {
  //   const currentProperties = this.properties.getValue();
  //   const newProperty: Property = {
  //     ...propertyData,
  //     id: Math.max(0, ...currentProperties.map(p => p.id)) + 1,
  //     name: `Property ${currentProperties.length + 1}`, // Temporary name
  //     type: this.getPropertyTypeLabel(propertyData.propertyType),
  //     units: '0',
  //     occupancy: '0%',
  //     status: 'Pending'
  //   };

  //   this.properties.next([...currentProperties, newProperty]);
  //   return this.getPropertyById(newProperty.id);
  // }







  // getPropertyById(id: number): Observable<Property> {
  //   return this.properties.pipe(
  //     map(properties => {
  //       const property = properties.find(p => p.id === id);
  //       if (!property) throw new Error('Property not found');
  //       return property;
  //     })
  //   );
  // }


  // deleteProperty(id: number): void {
  //   const currentProperties = this.properties.getValue();
  //   this.properties.next(currentProperties.filter(p => p.id !== id));
  // }








  
  private getPropertyTypeLabel(type: string): string {
    const labels: Record<string, string> = {
      house: 'Single Family',
      condo: 'Condo/Apartment',
      townhouse: 'Townhouse',
      community: 'Multi-family'
    };
    return labels[type] || type;
  }




}