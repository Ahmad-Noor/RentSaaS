import { inject, Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Company } from '../types/company.types';
import { environment } from '../../../../../../environments/environment.development';
import { isPlatformBrowser } from '@angular/common';
import { CompanyCreate } from '../pages/Inteface/Company/company-create';

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
  platformId=inject(PLATFORM_ID);
  constructor(
    private _httpClient: HttpClient,
  ) {
    this.initializeHeaders();
  }

  private initializeHeaders(): void {
    if (isPlatformBrowser(this.platformId)) {
      const token = localStorage.getItem("token");
      const organizationId = localStorage.getItem("organizationId");
      
      if (token && organizationId) {
        this.headers = new HttpHeaders({
          "Content-Type": "application/json",
          "X-OrganizationId": organizationId,
          Authorization: `Bearer ${token}`,
        });
      } else {
        console.error("Missing token or organizationId in localStorage.");
        this.headers = new HttpHeaders(); // Fallback in case of missing values
      }
    } else {
      this.headers = new HttpHeaders(); // Empty headers for non-browser platforms
    }
  }
  
  private ensureHeadersInitialized(): void {
    if (!this.headers) {
      console.log('Initializing headers...');
      this.initializeHeaders();
    } else {
      console.log('Headers already initialized.');
    }
  }
  

  getCompanies(): Observable<any> {
    this.ensureHeadersInitialized();


    console.log(this.headers)
    return this._httpClient.get(`${this.baseUrl}api/Company`, { headers: this.headers });
  }

  addCompany(companyCreate: CompanyCreate): Observable<any> {
    this.ensureHeadersInitialized();
  
    return this._httpClient.post(`${this.baseUrl}api/Company/Add`, companyCreate,{headers:this.headers});
  }

  updateCompany(id: number, updates: Partial<Company>): void {
    const currentCompanies = this.companies.getValue();
    const updatedCompanies = currentCompanies.map((company) =>
      company.id === id ? { ...company, ...updates } : company
    );
    this.companies.next(updatedCompanies);
  }

  deleteCompany(id: number): Observable<void> {
    this.ensureHeadersInitialized();
    return this._httpClient.delete<void>(`/api/companies/${id}`, { headers: this.headers });
  }
}