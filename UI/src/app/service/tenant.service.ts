import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { UserService } from "./user.service";
import { TenantCreate,Tenant } from "../models/tenant.model";
import { environment } from "../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
@Injectable({
  providedIn: 'root'
})
export class TenantService {
  apiUrl: string = environment.apiUrl + "api/tenant";
  headers: HttpHeaders = new HttpHeaders();

  constructor(private http: HttpClient, private userService: UserService) {
    this.headers = new HttpHeaders({
      "X-OrganizationId": `${userService.getCurrentOrganizationId()}`,
      Authorization: `Bearer ${userService.getToken()}`,
    });
  }



  addTenant(data: TenantCreate): Observable<any> {
    return this.http.post(`${this.apiUrl}`, data, {
      headers: this.headers,
    });
  }


  getAllTenanties(): Observable<any> {
    return this.http.get(`${this.apiUrl}`, {
      headers: this.headers,
    });
  }

  getTenantById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/GetById/${id}`, {
      headers: this.headers,
    });
  }



  updateTenant(id: string, data: Tenant): Observable<any> {
    return this.http.put(`${this.apiUrl}/Update/${id}`, data, {
      headers: this.headers,
    });
  }

  deleteTenant(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/delete/${id}`, {
      headers: this.headers,
    });
  }

}
