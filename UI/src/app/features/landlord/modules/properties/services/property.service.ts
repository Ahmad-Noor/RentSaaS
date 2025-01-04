import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { Property, CreatePropertyDTO } from '../types/property.types';
import { MOCK_PROPERTIES } from '../data/mock-properties';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  private properties = new BehaviorSubject<Property[]>(MOCK_PROPERTIES);

  getProperties(): Observable<Property[]> {
    return this.properties.asObservable();
  }

  addProperty(propertyData: CreatePropertyDTO): Observable<Property> {
    const currentProperties = this.properties.getValue();
    const newProperty: Property = {
      ...propertyData,
      id: Math.max(0, ...currentProperties.map(p => p.id)) + 1,
      name: `Property ${currentProperties.length + 1}`, // Temporary name
      type: this.getPropertyTypeLabel(propertyData.propertyType),
      units: '0',
      occupancy: '0%',
      status: 'Pending'
    };

    this.properties.next([...currentProperties, newProperty]);
    return this.getPropertyById(newProperty.id);
  }

  getPropertyById(id: number): Observable<Property> {
    return this.properties.pipe(
      map(properties => {
        const property = properties.find(p => p.id === id);
        if (!property) throw new Error('Property not found');
        return property;
      })
    );
  }

  deleteProperty(id: number): void {
    const currentProperties = this.properties.getValue();
    this.properties.next(currentProperties.filter(p => p.id !== id));
  }

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