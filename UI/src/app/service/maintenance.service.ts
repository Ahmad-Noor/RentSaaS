import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { MaintenanceRequest } from '../models/maintenance.types';
import { MOCK_REQUESTS } from '../landlord/maintenance/data/mock-requests';

@Injectable({
  providedIn: 'root'
})
export class MaintenanceService {
  private requests = new BehaviorSubject<MaintenanceRequest[]>(MOCK_REQUESTS);

  getRequests(): Observable<MaintenanceRequest[]> {
    return this.requests.asObservable();
  }

  createRequest(request: Omit<MaintenanceRequest, 'id' | 'status' | 'createdAt' | 'updatedAt'>): void {
    const currentRequests = this.requests.getValue();
    const newRequest: MaintenanceRequest = {
      ...request,
      id: Math.max(0, ...currentRequests.map(r => r.id || 0)) + 1,
      status: 'pending',
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString()
    };
    
    this.requests.next([...currentRequests, newRequest]);
  }

  updateRequest(id: number, updates: Partial<MaintenanceRequest>): void {
    const currentRequests = this.requests.getValue();
    const updatedRequests = currentRequests.map(request => 
      request.id === id 
        ? { ...request, ...updates, updatedAt: new Date().toISOString() }
        : request
    );
    
    this.requests.next(updatedRequests);
  }

  deleteRequest(id: number): void {
    const currentRequests = this.requests.getValue();
    this.requests.next(currentRequests.filter(request => request.id !== id));
  }
}