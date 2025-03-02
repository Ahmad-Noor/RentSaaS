import { Observable } from "rxjs";
import { UserService } from "./user.service";
import { isPlatformBrowser } from "@angular/common";
import { CompanyCreate } from "../models/company-create";
import { environment } from "../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable, PLATFORM_ID } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class CompanyService {
  baseUrl = environment.apiUrl;
  private headers!: HttpHeaders;
  platformId = inject(PLATFORM_ID);
  constructor(
    private _httpClient: HttpClient,
    private userService: UserService
  ) {
    this.initializeHeaders();
  }

  private initializeHeaders(): void {
    if (isPlatformBrowser(this.platformId)) { 
      
      this.headers = new HttpHeaders({
        "Content-Type": "application/json",
        "X-OrganizationId": this.userService.getCurrentOrganizationId() || "",
        Authorization: `Bearer ${this.userService.getToken()}`,
      });
    } else {
      this.headers = new HttpHeaders(); // Empty headers for non-browser platforms
    }
  }

  private ensureHeadersInitialized(): void {
    if (!this.headers) { 
      this.initializeHeaders();
    }  
  }

  getCompanies(): Observable<any> {
    this.ensureHeadersInitialized();
    return this._httpClient.get(`${this.baseUrl}api/Company`, {
      headers: this.headers,
    });
  }

  addCompany(companyCreate: CompanyCreate): Observable<any> {
    this.ensureHeadersInitialized();

    return this._httpClient.post(
      `${this.baseUrl}api/Company/Add`,
      companyCreate,
      { headers: this.headers }
    );
  }

  deleteCompany(id: string): Observable<any> {
    this.ensureHeadersInitialized();
    return this._httpClient.delete(`${this.baseUrl}api/Company/${id}`, {
      headers: this.headers,
    });
  }

  // updateCompany(id: number, updates: Partial<Company>): void {
  //   const currentCompanies = this.companies.getValue();
  //   const updatedCompanies = currentCompanies.map((company) =>
  //     company.id === id ? { ...company, ...updates } : company
  //   );
  //   this.companies.next(updatedCompanies);
  // }
}
