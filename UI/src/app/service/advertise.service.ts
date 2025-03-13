import { Observable } from "rxjs";
import { map } from 'rxjs/operators';
import { Injectable } from "@angular/core";
import { UserService } from "./user.service";
import {  Advertising} from "../models/advertising.types";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { APIResponse } from "../models/api-response.types";

@Injectable({
  providedIn: "root",
})
export class AdvertisingService {
  apiUrl: string = `${environment.apiUrl}api/Advertising`;
  private headers!: HttpHeaders;

  constructor(private http: HttpClient, private _userService: UserService) {
    this.headers = new HttpHeaders({
      "X-OrganizationId": this._userService.getCurrentOrganizationId() || "",
      Authorization: `Bearer ${this._userService.getToken()}`,
    });
  }

  getAllAdvertisements(): Observable<Advertising[]> {
    return this.http
      .get<APIResponse<Advertising[]>>(this.apiUrl, { headers: this.headers })
      .pipe(
        map(response => response.data)
      );
  }

  getAdvertisementById(id: string): Observable<Advertising> {
    return this.http.get<Advertising>(`${this.apiUrl}/${id}`, { headers: this.headers });
  }

  addAdvertisement(adData: Advertising, files?: File[]): Observable<Advertising> {
    const formData = new FormData();

    for (const key in adData) {
      if (adData.hasOwnProperty(key) && (adData as any)[key] !== null) {
        if (key === 'propertyPhotos' && Array.isArray((adData as any)[key])) {
          continue;
        } else if (typeof (adData as any)[key] === 'object') {
          formData.append(key, JSON.stringify((adData as any)[key]));
        } else {
          formData.append(key, (adData as any)[key].toString());
        }
      }
    }

    if (files && files.length > 0) {
      files.forEach(file => {
        formData.append('Files', file, file.name);
      });
    }

    const headers = new HttpHeaders({
      "X-OrganizationId": this._userService.getCurrentOrganizationId() || "",
      Authorization: `Bearer ${this._userService.getToken()}`,
    });

    return this.http.post<Advertising>(this.apiUrl, formData, { headers });
  }

  updateAdvertisement(adId: string, adData: Advertising, files?: File[]): Observable<Advertising> {
    const formData = new FormData();

    for (const key in adData) {
      if (adData.hasOwnProperty(key) && (adData as any)[key] !== null) {
        if (key === 'filesToDelete' && Array.isArray((adData as any)[key])) {
          (adData as any)[key].forEach((fileId: string) => {
            formData.append('FilesToDelete', fileId);
          });
        } else if (typeof (adData as any)[key] === 'object' && !((adData as any)[key] instanceof File)) {
          formData.append(key, JSON.stringify((adData as any)[key]));
        } else {
          formData.append(key, (adData as any)[key]);
        }
      }
    }

    if (files && files.length > 0) {
      files.forEach(file => {
        formData.append('files', file, file.name);
      });
    }

    const headers = new HttpHeaders({
      "X-OrganizationId": this._userService.getCurrentOrganizationId() || "",
      Authorization: `Bearer ${this._userService.getToken()}`,
    });

    return this.http.put<Advertising>(`${this.apiUrl}/${adId}`, formData, { headers });
  }

  deleteAdvertisement(adId: string): Observable<Advertising> {
    return this.http.delete<Advertising>(`${this.apiUrl}/${adId}`, { headers: this.headers });
  }
}