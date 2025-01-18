import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { LoginResponse, SocialLoginResponse } from '../types/auth.types';
import {LoginInterface} from '../../../interfaces/LoginInterface';
import {RegisterInterface} from '../../../interfaces/RegisterInterface';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

baseUrl:string="https://localhost:7164/api/Auth/";


constructor(private _HttpClient:HttpClient) {
  

  
}

// getWithOrganizationHeader() {
//   const headers = new HttpHeaders({
//     'Content-Type': 'application/json',
//     'Authorization': 'Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjE0ZDNhZjQ5LTRjY2MtNDVkZi1iMWQyLWZiMDg1MTcwODc5MCIsInN1YiI6IkhhcmVkeXNzQHJlbnRzYWFzLmNvbSIsImVtYWlsIjoiSGFyZWR5c3NAcmVudHNhYXMuY29tIiwiZ2l2ZW5fbmFtZSI6Ik1vaGFtZWRzIEhhcmVkeXNzIiwianRpIjoiODEwNzEyNjgtNzUxZC00ZDdkLThmMDktNDk0MDI4YmZlYWI5IiwibmJmIjoxNzM2MjYxNDg0LCJleHAiOjE3MzYyNjE0OTQsImlhdCI6MTczNjI2MTQ4NH0.mRwD576CkXmCYnu3sK0b4shujpjXIGGcmqas1MjwjBRo0mdb6ZbSpONsZpfyiP7c3FRv9i1unlQ0sEU0OycQ3w',
//     'X-OrganizationId': '00000000-0000-0000-0000-000000000001'
//   });

//   return this._HttpClient.get(`${this.baseUrl}get`, { headers });
// }





  login(LoginInterfaces:LoginInterface): Observable<any> {

    return this._HttpClient.post(`${this.baseUrl}login`,LoginInterfaces);

  }



  

  Register(RegisterInterface:RegisterInterface): Observable<any> {
    // TODO: Implement actual authentication
    return this._HttpClient.post(`${this.baseUrl}Register`,RegisterInterface);
  }








  loginWithGoogle(): Observable<SocialLoginResponse> {
    return of({ 
      success: true,
      provider: 'google',
      token: 'mock-google-token'
    });
  }

  loginWithFacebook(): Observable<SocialLoginResponse> {
    return of({ 
      success: true,
      provider: 'facebook',
      token: 'mock-facebook-token'
    });
  }

  loginWithApple(): Observable<SocialLoginResponse> {
    return of({ 
      success: true,
      provider: 'apple',
      token: 'mock-apple-token'
    });
  }









}