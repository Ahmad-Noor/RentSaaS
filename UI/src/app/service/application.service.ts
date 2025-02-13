import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs'; 
import { Application } from '../models/application.types';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {
  private applications = new BehaviorSubject<Application[]>([
    {
      id: 1,
      propertyId: 1,
      propertyName: 'Sunset Apartments',
      applicantName: 'John Smith',
      email: 'john.smith@example.com',
      phone: '(555) 123-4567',
      status: 'new',
      submittedAt: '2024-01-15T10:30:00Z',
      desiredMoveIn: '2024-02-01',
      creditScore: 720,
      income: 75000
    },
    {
      id: 2,
      propertyId: 2,
      propertyName: 'Downtown Lofts',
      applicantName: 'Sarah Johnson',
      email: 'sarah.j@example.com',
      phone: '(555) 987-6543',
      status: 'reviewing',
      submittedAt: '2024-01-14T15:45:00Z',
      desiredMoveIn: '2024-03-01',
      creditScore: 680,
      income: 65000
    }
  ]);

  getApplications(): Observable<Application[]> {
    return this.applications.asObservable();
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
}