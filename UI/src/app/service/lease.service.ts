import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CreateLeaseDTO, Lease } from '../models/lease.types';
// import { Lease, CreateLeaseDTO } from '../../types/lease.types';

@Injectable({
  providedIn: 'root'
})
export class LeaseService {
  private leases = new BehaviorSubject<Lease[]>([
    {
      id: 1,
      propertyId: 1,
      propertyName: 'Sunset Apartments',
      tenantName: 'John Smith',
      type: 'standard',
      startDate: '2024-02-01',
      endDate: '2025-01-31',
      monthlyRent: 1500,
      status: 'signed',
      createdAt: '2024-01-15T10:30:00Z',
      updatedAt: '2024-01-15T10:30:00Z'
    },
    {
      id: 2,
      propertyId: 2,
      propertyName: 'Downtown Lofts',
      tenantName: 'Sarah Johnson',
      type: 'month-to-month',
      startDate: '2024-03-01',
      endDate: '2024-03-31',
      monthlyRent: 2000,
      status: 'sent',
      createdAt: '2024-01-14T15:45:00Z',
      updatedAt: '2024-01-14T15:45:00Z'
    }
  ]);

  getLeases(): Observable<Lease[]> {
    return this.leases.asObservable();
  }

  createLease(data: CreateLeaseDTO): void {
    const currentLeases = this.leases.getValue();
    const newLease: Lease = {
      ...data,
      id: Math.max(0, ...currentLeases.map(l => l.id)) + 1,
      propertyName: 'Property Name', // This would come from a property service
      status: 'draft',
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString()
    };
    
    this.leases.next([...currentLeases, newLease]);
  }

  updateLeaseStatus(id: number, status: Lease['status']): void {
    const currentLeases = this.leases.getValue();
    const updatedLeases = currentLeases.map(lease => 
      lease.id === id 
        ? { ...lease, status, updatedAt: new Date().toISOString() }
        : lease
    );
    
    this.leases.next(updatedLeases);
  }

  deleteLease(id: number): void {
    const currentLeases = this.leases.getValue();
    this.leases.next(currentLeases.filter(lease => lease.id !== id));
  }
}