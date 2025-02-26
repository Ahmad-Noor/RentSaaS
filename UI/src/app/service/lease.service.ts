import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable } from 'rxjs';
import { APIResponse } from '../models/api-response.types';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Lease } from '../models/lease.types';
import { UserService } from './user.service';

 
@Injectable({
  providedIn: 'root'
})
export class LeaseService {
  apiUrl: string = `${environment.apiUrl}api/Lease`; 

  constructor(private http: HttpClient, private userService: UserService) {}

  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'X-OrganizationId': `${this.userService.getCurrentOrganizationId()}`,
      'Authorization': `Bearer ${this.userService.getToken()}`,
      'Content-Type': 'application/json'
    });
  }


  getLeases(): Observable<Lease[]> {
    return this.http
      .get<APIResponse<Lease[]>>(this.apiUrl, { headers: this.getHeaders()})
      .pipe(
        map(response => response.data)
      );
  }


  getLeaseById(id: string): Observable<Lease> {
    return this.http.get<Lease>(`${this.apiUrl}/${id}`, {headers: this.getHeaders()});
  }


  addLease(data: Lease): Observable<APIResponse<Lease>> {
    return this.http.post<APIResponse<Lease>>(this.apiUrl, data, {
      headers: this.getHeaders()
    }).pipe(
      catchError(error => {
        console.error('Lease creation error:', error);
        throw error;
      })
    );
  }

  updateLease(id: string, data: Lease): Observable<Lease> {
    return this.http.put<Lease>(`${this.apiUrl}/${id}`, data, {headers: this.getHeaders()});

  }

  deleteLease(id: string): Observable<Lease> {
    return this.http.delete<Lease>(`${this.apiUrl}/${id}`, {headers: this.getHeaders()});

  }
}