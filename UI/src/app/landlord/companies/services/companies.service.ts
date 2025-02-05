import { inject, Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Company } from '../types/company.types';
import { environment } from '../../../../environments/environment';
import { isPlatformBrowser } from '@angular/common';
import { CompanyCreate } from '../Company/company-create';
import { UUID } from 'crypto';

@Injectable({
  providedIn: "root",
})
export class CompaniesService {


  baseUrl = environment.apiUrl;
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

  deleteCompany(id: string): Observable<any> {
    this.ensureHeadersInitialized();
    return this._httpClient.delete(`${this.baseUrl}api/Company/${id}`, { headers: this.headers });
  }





  // updateCompany(id: number, updates: Partial<Company>): void {
  //   const currentCompanies = this.companies.getValue();
  //   const updatedCompanies = currentCompanies.map((company) =>
  //     company.id === id ? { ...company, ...updates } : company
  //   );
  //   this.companies.next(updatedCompanies);
  // }






}