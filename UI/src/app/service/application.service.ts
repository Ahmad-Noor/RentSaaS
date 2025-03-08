import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable } from 'rxjs';
import { APIResponse } from '../models/api-response.types';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Application } from '../models/application.types';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root',
})
export class ApplicationService {
  apiUrl: string = `${environment.apiUrl.replace(/\/$/, '')}/api/ApplicationAndLeads`;
  constructor(private http: HttpClient, private userService: UserService) { }

  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'X-OrganizationId': `${this.userService.getCurrentOrganizationId()}`,
      'Authorization': `Bearer ${this.userService.getToken()}`,
      'Content-Type': 'application/json'
    });
  }


  getApplications(): Observable<Application[]> {
    return this.http
      .get<APIResponse<Application[]>>(this.apiUrl, { headers: this.getHeaders() })
      .pipe(
        map(response => response.data)
      );
  }


  getApplicationById(id: string): Observable<Application> {
    return this.http.get<Application>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }


  addApplication(data: Application): Observable<APIResponse<Application>> {
    return this.http.post<APIResponse<Application>>(this.apiUrl, data, {
      headers: this.getHeaders()
    }).pipe(
      catchError(error => {
        console.error('Lease creation error:', error);
        throw error;
      })
    );
  }

  updateApplication(id: string, data: Application): Observable<Application> {
    return this.http.put<Application>(`${this.apiUrl}/${id}`, data, { headers: this.getHeaders() });

  }

  deleteApplication(id: string): Observable<Application> {
    return this.http.delete<Application>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });

  }

}

