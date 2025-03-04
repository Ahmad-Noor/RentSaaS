import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators'; 
import { UserService } from "./user.service";
import { Payment } from "../models/payment.types";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { APIResponse } from "../models/api-response.types";


@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  apiUrl: string = `${environment.apiUrl}api/payment`;
  private headers!: HttpHeaders;

  constructor(private http: HttpClient, private _userService: UserService) {
    this.headers = new HttpHeaders({
    //  "Content-Type": "application/json",
      "X-OrganizationId": this._userService.getCurrentOrganizationId() || "",
      Authorization: `Bearer ${this._userService.getToken()}`,
    });
   }

  getAllPayment(): Observable<Payment[]> {
    return this.http
      .get<APIResponse<Payment[]>>(this.apiUrl, { headers: this.headers })
      .pipe(
        map(response => response.data)
      );
  }
    getPaymentById(id: string): Observable<Payment> {

    return this.http.get<Payment>(`${this.apiUrl}/${id}`, {headers: this.headers});
  }

  addPayment(paymentData: Payment, files?: File[]): Observable<Payment> {
    const formData = new FormData();
    
    // Add expense data fields
    for (const key in paymentData) {
      if (paymentData.hasOwnProperty(key) && (paymentData as any)[key] !== null) {
        // Handle different data types appropriately
        if (key === 'files' && Array.isArray((paymentData as any)[key])) {
          continue; // Skip the receipts array as we'll handle files separately
        } else if (typeof (paymentData as any)[key] === 'object') {
          formData.append(key, JSON.stringify((paymentData as any)[key]));
        } else {
          formData.append(key, (paymentData as any)[key].toString());
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
    
    return this.http.post<Payment>(this.apiUrl, formData, {headers});
  }
  updatePayment(paymentId: string, paymentData: Payment, files?: File[]): Observable<Payment> {
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
    
    for (const key in paymentData) {
      if (paymentData.hasOwnProperty(key) && (paymentData as any)[key] !== null) {
        if (key === 'filesToDelete' && Array.isArray((paymentData as any)[key])) {
          // Append each GUID in the FilesToDelete array individually
          (paymentData as any)[key].forEach((fileId: string) => {
            formData.append('FilesToDelete', fileId);
          });
        } else if (typeof (paymentData as any)[key] === 'object' && !((paymentData as any)[key] instanceof File)) {
          formData.append(key, JSON.stringify((paymentData as any)[key]));
        } else {
          formData.append(key, (paymentData as any)[key]);
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
    
    return this.http.put<Payment>(`${this.apiUrl}/${paymentId}`, formData, {headers});
  }
  
  deletePayment(paymentId: string): Observable<Payment> {
    return this.http.delete<Payment>(`${this.apiUrl}/${paymentId}`, {headers: this.headers});
  }
}