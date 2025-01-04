import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { LoginResponse, SocialLoginResponse } from '../types/auth.types';
import {LoginInterface} from '../../../interfaces/LoginInterface';
import {RegisterInterface} from '../../../interfaces/RegisterInterface';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

baseUrl:string="https://localhost:7164/api/Auth/";


constructor(private _HttpClient:HttpClient) {
  

  
}




  login(LoginInterfaces:LoginInterface): Observable<any> {
    // TODO: Implement actual authentication
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