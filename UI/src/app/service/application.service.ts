import { Injectable } from '@angular/core';
import {  map, Observable } from 'rxjs'; 
import { Application } from '../models/application.types';

 import { HttpClient, HttpHeaders } from '@angular/common/http';
 import { UserService } from './user.service';
import { environment } from '../../environments/environment';
import { APIResponse } from '../models/api-response.types';
 
@Injectable({
  providedIn: 'root',
})
export class ApplicationService {
  apiUrl: string = `${environment.apiUrl.replace(/\/$/, '')}/api/ApplicationAndLeads`;
  headers: HttpHeaders = new HttpHeaders();

  constructor(private http: HttpClient, private userService: UserService) {
    this.headers = new HttpHeaders({
      "X-OrganizationId": `${this.userService.getCurrentOrganizationId()}`,
      Authorization: `Bearer ${this.userService.getToken()}`,
    });
  }

  getAllApplications(): Observable<Application[]> {
    return this.http.get<APIResponse<Application[]>>(this.apiUrl, { headers: this.headers }).pipe(
      map(response => response.data ?? [])  
    );
  }

  // getAllApplications(): Observable<Application[]> {
  //     return this.http
  //       .get<APIResponse<Application[]>>(this.apiUrl, { headers: this.headers })
  //       .pipe(
  //         map(response => response.data)
  //       );
  //   }

  getApplicationById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`, {
      headers: this.headers,
    });
  }


  addApplication(data: Application): Observable<Application> {
    return this.http.post<Application>(this.apiUrl, data, {headers: this.headers});
  }

  updateApplication(id: string, data: Application): Observable<Application> {
      return this.http.put<Application>(`${this.apiUrl}/${id}`, data, {headers: this.headers});
    }
  
    deleteApplication(id: string): Observable<Application> {
      return this.http.delete<Application>(`${this.apiUrl}/${id}`, {headers: this.headers});
    }

      // getAllApplications(): Observable<any> {
  //   return this.http.get(`${this.apiUrl}`, {
  //     headers: this.headers,
  //   });
  // }
  // addApplication(data: Application): Observable<any> {
  //   return this.http.post(`${this.apiUrl}`, data, {
  //     headers: this.headers,
  //   });
  // }

//  updateApplication(id: string, data: Application): Observable<any> {
//    return this.http.put(`${this.apiUrl}/${id}`, data, {
//      headers: this.headers,
//    });
//  }

//  deleteApplication(id: string): Observable<any> {
//    return this.http.delete(`${this.apiUrl}/${id}`, {
//      headers: this.headers,
//    });
//  }

}

