import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs'; 
import { Application, ApplicationCreate } from '../models/application.types';

 import { HttpClient, HttpHeaders } from '@angular/common/http';
 import { UserService } from './user.service';
import { environment } from '../../environments/environment';
 
@Injectable({
  providedIn: 'root',
})
export class ApplicationService {
  apiUrl: string = environment.apiUrl + "api/ApplicationAndLeads";
  headers: HttpHeaders = new HttpHeaders();

  constructor(private http: HttpClient, private userService: UserService) {
    this.headers = new HttpHeaders({
      "X-OrganizationId": `${userService.getCurrentOrganizationId()}`,
      Authorization: `Bearer ${userService.getToken()}`,
    });
  }

  getAllApplications(): Observable<any> {
    return this.http.get(`${this.apiUrl}`, {
      headers: this.headers,
    });
  }

  getApplicationById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`, {
      headers: this.headers,
    });
  }

  addApplication(data: Application): Observable<any> {
    return this.http.post(`${this.apiUrl}`, data, {
      headers: this.headers,
    });
  }

 updateApplication(id: string, data: Application): Observable<any> {
   return this.http.put(`${this.apiUrl}/${id}`, data, {
     headers: this.headers,
   });
 }

 deleteApplication(id: string): Observable<any> {
   return this.http.delete(`${this.apiUrl}/${id}`, {
     headers: this.headers,
   });
 }
}
