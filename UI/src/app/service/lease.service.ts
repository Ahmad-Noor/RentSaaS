import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
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
  headers!: HttpHeaders;

constructor(private http: HttpClient, private userService: UserService) {
  this.headers = new HttpHeaders({
    "X-OrganizationId": `${this.userService.getCurrentOrganizationId()}`,
    Authorization: `Bearer ${this.userService.getToken()}`,
  });
}


  getLeases(): Observable<Lease[]> {
    return this.http
      .get<APIResponse<Lease[]>>(this.apiUrl, { headers: this.headers })
      .pipe(
        map(response => response.data)
      );
  }


  getLeaseById(id: string): Observable<Lease> {
    return this.http.get<Lease>(`${this.apiUrl}/${id}`, {headers: this.headers});
  }


  addLease(data: Lease): Observable<Lease>  {
    console.log(data);
    console.log(this.apiUrl);
    console.log(this.headers);
    return this.http.post<Lease>(this.apiUrl, data, {headers: this.headers});
  }

  updateLease(id: string, data: Lease): Observable<Lease> {
    return this.http.put<Lease>(`${this.apiUrl}/${id}`, data, {headers: this.headers});

  }

  deleteLease(id: string): Observable<Lease> {
    return this.http.delete<Lease>(`${this.apiUrl}/${id}`, {headers: this.headers});

  }
}