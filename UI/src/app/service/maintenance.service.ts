import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { MaintenanceRequest } from '../models/maintenance.types';
import { map } from 'rxjs/operators';
import { UserService } from "./user.service";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { APIResponse } from "../models/api-response.types";

@Injectable({
  providedIn: 'root'
})
export class MaintenanceService {
  apiUrl: string = `${environment.apiUrl}api/maintenancerequest`;
  private headers!: HttpHeaders;

  constructor(private http: HttpClient, private _userService: UserService) {
    this.headers = new HttpHeaders({
      //  "Content-Type": "application/json",
      "X-OrganizationId": this._userService.getCurrentOrganizationId() || "",
      Authorization: `Bearer ${this._userService.getToken()}`,
    });
  }

  getMaintenanceRequestById(id: string): Observable<MaintenanceRequest> {

    return this.http.get<MaintenanceRequest>(`${this.apiUrl}/${id}`, { headers: this.headers });
  }

  getAllMaintenanceRequest(): Observable<MaintenanceRequest[]> {
    return this.http
      .get<APIResponse<MaintenanceRequest[]>>(this.apiUrl, { headers: this.headers })
      .pipe(
        map(response => response.data)
      );
  }

  getMaintenanceById(id: string): Observable<MaintenanceRequest> {

    return this.http.get<MaintenanceRequest>(`${this.apiUrl}/${id}`, { headers: this.headers });
  }

  addMaintenance(maintenanceData: MaintenanceRequest, files?: File[]): Observable<MaintenanceRequest> {
    const formData = new FormData();

    // Add expense data fields
    for (const key in maintenanceData) {
      if (maintenanceData.hasOwnProperty(key) && (maintenanceData as any)[key] !== null) {
        // Handle different data types appropriately
        if (key === 'files' && Array.isArray((maintenanceData as any)[key])) {
          continue; // Skip the receipts array as we'll handle files separately
        } else if (typeof (maintenanceData as any)[key] === 'object') {
          formData.append(key, JSON.stringify((maintenanceData as any)[key]));
        } else {
          formData.append(key, (maintenanceData as any)[key].toString());
        }
      }
    }

    // Add receipt files
    if (files && files.length > 0) {
      files.forEach(file => {
        formData.append('Files', file, file.name);
      });
    }


    // Create headers without Content-Type (browser will set it automatically for FormData)
    const headers = new HttpHeaders({
      "X-OrganizationId": this._userService.getCurrentOrganizationId() || "",
      Authorization: `Bearer ${this._userService.getToken()}`,
    });

    return this.http.post<MaintenanceRequest>(this.apiUrl, formData, { headers });
  }
  updateMaintenance(maintenanceId: string, maintenanceData: MaintenanceRequest, files?: File[]): Observable<MaintenanceRequest> {
    const formData = new FormData();

    // Add expense data fields
    // for (const key in expenseData) {
    //   if (expenseData.hasOwnProperty(key) && (expenseData as any)[key] !== null) {
    //     if (typeof (expenseData as any)[key] === 'object' && !((expenseData as any)[key] instanceof File)) {
    //       formData.append(key, JSON.stringify((expenseData as any)[key]));
    //     } else {
    //       formData.append(key, (expenseData as any)[key]);
    //     }
    //   }
    // }

    for (const key in maintenanceData) {
      if (maintenanceData.hasOwnProperty(key) && (maintenanceData as any)[key] !== null) {
        if (key === 'filesToDelete' && Array.isArray((maintenanceData as any)[key])) {
          // Append each GUID in the FilesToDelete array individually
          (maintenanceData as any)[key].forEach((fileId: string) => {
            formData.append('FilesToDelete', fileId);
          });
        } else if (typeof (maintenanceData as any)[key] === 'object' && !((maintenanceData as any)[key] instanceof File)) {
          formData.append(key, JSON.stringify((maintenanceData as any)[key]));
        } else {
          formData.append(key, (maintenanceData as any)[key]);
        }
      }
    }

    // Add receipt files
    if (files && files.length > 0) {
      files.forEach(file => {
        formData.append('files', file, file.name);
      });
    }

    const headers = new HttpHeaders({
      "X-OrganizationId": this._userService.getCurrentOrganizationId() || "",
      Authorization: `Bearer ${this._userService.getToken()}`,
    });

    return this.http.put<MaintenanceRequest>(`${this.apiUrl}/${maintenanceId}`, formData, { headers });
  }

  deleteMaintenance(maintenanceId: string): Observable<MaintenanceRequest> {
    return this.http.delete<MaintenanceRequest>(`${this.apiUrl}/${maintenanceId}`, { headers: this.headers });
  }
}