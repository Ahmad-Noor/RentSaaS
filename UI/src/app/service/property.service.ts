import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { UserService } from "./user.service";
import { Property } from "../models/property.model";
import { environment } from "../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: "root",
})

export class PropertyService {
  apiUrl: string = environment.apiUrl + "api/property";
  headers: HttpHeaders = new HttpHeaders();

  constructor(private http: HttpClient, private userService: UserService) {
    this.headers = new HttpHeaders({
      "X-OrganizationId": `${userService.getCurrentOrganizationId()}`,
      Authorization: `Bearer ${userService.getToken()}`,
    });
  }

  getAllProperties(): Observable<Property[]> {
    return this.http.get<Property[]>(`${this.apiUrl}/GetAll`, {
      headers: this.headers,
    });
  }

  getPropertyById(id: number): Observable<Property> {
    return this.http.get<Property>(`${this.apiUrl}/${id}`, {
      headers: this.headers,
    });
  }

  addProperty(data: Property): Observable<Property> {
    return this.http.post<Property>(`${this.apiUrl}/Add`, data, {
      headers: this.headers,
    });
  }

  updateProperty(id: string, data: Property): Observable<Property> {
    return this.http.put<Property>(this.apiUrl, data, {
      headers: this.headers,
    });
  }

  deleteProperty(id: string): Observable<Property> {
    return this.http.delete<Property>(`${this.apiUrl}/${id}`, {
      headers: this.headers,
    });
  }

  private getPropertyTypeLabel(type: string): string {
    const labels: Record<string, string> = {
      house: "Single Family",
      condo: "Condo/Apartment",
      townhouse: "Townhouse",
      community: "Multi-family",
    };
    return labels[type] || type;
  }
}
