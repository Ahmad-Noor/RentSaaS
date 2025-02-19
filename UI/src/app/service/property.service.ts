import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { UserService } from "./user.service";
import { Property, PropertyCreate } from "../models/property.model";
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



  addProperty(data: PropertyCreate): Observable<any> {
    return this.http.post(`${this.apiUrl}/Add`, data, {
      headers: this.headers,
    });
  }


  getAllProperties(): Observable<any> {
    return this.http.get(`${this.apiUrl}/GetAll`, {
      headers: this.headers,
    });
  }

  getPropertyById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/GetById/${id}`, {
      headers: this.headers,
    });
  }



  updateProperty(id: string, data: Property): Observable<any> {
    return this.http.put(`${this.apiUrl}/Update/${id}`, data, {
      headers: this.headers,
    });
  }

  deleteProperty(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/delete/${id}`, {
      headers: this.headers,
    });
  }


  
}
