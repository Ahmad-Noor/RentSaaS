import { Inject, inject, Injectable, PLATFORM_ID } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Company } from '../types/company.types';
import { environment } from '../../../../../../environments/environment.development';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: "root",
})
export class CompaniesService {
  private companies = new BehaviorSubject<Company[]>([
    {
      id: 1,
      name: "Skyline Properties",
      type: "Property Management",
      properties: "12",
      employees: "45",
      status: "Active",
    },
    {
      id: 2,
      name: "Urban Living",
      type: "Real Estate",
      properties: "8",
      employees: "23",
      status: "Active",
    },
    {
      id: 3,
      name: "Metro Rentals",
      type: "Property Management",
      properties: "15",
      employees: "32",
      status: "Active",
    },
  ]);
  baseUrl = environment.baseUrl;
  private headers!: HttpHeaders;

  constructor(
    private _httpclint: HttpClient,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.initializeHeaders(); // Initialize headers here
  }

  private initializeHeaders(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.headers = new HttpHeaders({
        "Content-Type": "application/json",
        "X-OrganizationId": `${localStorage.getItem("organizationId")}`,
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      });

      console.log(this.headers);
    } else {
      this.headers = new HttpHeaders(); // Empty headers for non-browser platforms
    }
  }

  getCompanies(): Observable<any> {
    return this._httpclint.get(`${this.baseUrl}api/Company`,{headers: this.headers});
  }

  addCompany(company: Omit<Company, "id">): void {
    const currentCompanies = this.companies.getValue();
    const newId = Math.max(...currentCompanies.map((c) => c.id)) + 1;

    this.companies.next([...currentCompanies, { ...company, id: newId }]);
  }

  updateCompany(id: number, updates: Partial<Company>): void {
    const currentCompanies = this.companies.getValue();
    const updatedCompanies = currentCompanies.map((company) =>
      company.id === id ? { ...company, ...updates } : company
    );

    this.companies.next(updatedCompanies);
  }

  deleteCompany(id: number): Observable<void> {
    return this._httpclint.delete<void>(`/api/companies/${id}`);
  }
}