import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Company } from '../types/company.types';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private companies: Company[] = [
    {
      id: 1,
      name: 'Skyline Properties',
      type: 'Property Management',
      properties: '12',
      employees: '45',
      status: 'Active'
    }
  ];

  getCompany(id: number): Observable<Company | undefined> {
    return of(this.companies.find(company => company.id === id));
  }

  updateCompany(id: number, data: Partial<Company>): Observable<Company> {
    const index = this.companies.findIndex(company => company.id === id);
    if (index !== -1) {
      this.companies[index] = { ...this.companies[index], ...data };
      return of(this.companies[index]);
    }
    throw new Error('Company not found');
  }

  createCompany(data: Omit<Company, 'id'>): Observable<Company> {
    const newCompany = {
      ...data,
      id: Math.max(...this.companies.map(c => c.id)) + 1
    };
    this.companies.push(newCompany);
    return of(newCompany);
  }
}