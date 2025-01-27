import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Company } from '../types/company.types';

@Injectable({
  providedIn: 'root'
})
export class CompaniesService {
  private companies = new BehaviorSubject<Company[]>([
    {
      id: 1,
      name: 'Skyline Properties',
      type: 'Property Management',
      properties: '12',
      employees: '45',
      status: 'Active'
    },
    {
      id: 2,
      name: 'Urban Living',
      type: 'Real Estate',
      properties: '8',
      employees: '23',
      status: 'Active'
    },
    {
      id: 3,
      name: 'Metro Rentals',
      type: 'Property Management',
      properties: '15',
      employees: '32',
      status: 'Active'
    }
  ]);

  constructor(private http: HttpClient) {}

  getCompanies(): Observable<Company[]> {
    return this.companies.asObservable();
  }

  addCompany(company: Omit<Company, 'id'>): void {
    const currentCompanies = this.companies.getValue();
    const newId = Math.max(...currentCompanies.map(c => c.id)) + 1;
    
    this.companies.next([
      ...currentCompanies,
      { ...company, id: newId }
    ]);
  }

  updateCompany(id: number, updates: Partial<Company>): void {
    const currentCompanies = this.companies.getValue();
    const updatedCompanies = currentCompanies.map(company => 
      company.id === id ? { ...company, ...updates } : company
    );
    
    this.companies.next(updatedCompanies);
  }

  deleteCompany(id: number): Observable<void> {
    return this.http.delete<void>(`/api/companies/${id}`);
  }
}