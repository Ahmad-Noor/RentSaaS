import { Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { BehaviorSubject, Observable, of } from "rxjs";
import {  SocialLoginResponse } from "../types/auth.types";
import { LoginInterface } from "../interfaces/LoginInterface";
import { RegisterInterface } from "../interfaces/RegisterInterface";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { jwtDecode } from "jwt-decode";
import { isPlatformBrowser } from "@angular/common";
import { environment } from "../../../environments/environment";
import { Constant } from "../../constants";
import { UserService } from "../../service/user.service";
@Injectable({
  providedIn: "root",
})
 
export class AuthService {
  baseUrl: string = environment.apiUrl + "api/Auth/";

  userData: BehaviorSubject<any> = new BehaviorSubject(null);

  constructor(private _HttpClient: HttpClient, private _router: Router, private userService: UserService, @Inject(PLATFORM_ID) private CheckPlatform:object) {
 
    if(isPlatformBrowser(this.CheckPlatform))
      {
        if(this.userService.getToken())
        {
          this.SaveData();
        }
      }
  }

  login(LoginInterfaces: LoginInterface): Observable<any> {
    return this._HttpClient.post(`${this.baseUrl}login`, LoginInterfaces);
  }

  Register(RegisterInterface: RegisterInterface): Observable<any> {
    // TODO: Implement actual authentication
    return this._HttpClient.post(`${this.baseUrl}Register`, RegisterInterface);
  }

  SignOut() {
    localStorage.removeItem(Constant.token);
    localStorage.removeItem(Constant.OrganizationIdRentSass);
    this._router.navigate(["/login"]);
  }

  SaveData(): void {
    this.userData.next(
      jwtDecode(JSON.stringify(this.userService.getToken()))
    ); 
  }

  loginWithGoogle(): Observable<SocialLoginResponse> {
    return of({
      success: true,
      provider: "google",
      token: "mock-google-token",
    });
  }

  loginWithFacebook(): Observable<SocialLoginResponse> {
    return of({
      success: true,
      provider: "facebook",
      token: "mock-facebook-token",
    });
  }

  loginWithApple(): Observable<SocialLoginResponse> {
    return of({
      success: true,
      provider: "apple",
      token: "mock-apple-token",
    });
  }

  // getWithOrganizationHeader() {
  //   const headers = new HttpHeaders({
  //     'Content-Type': 'application/json',
  //     'Authorization': 'Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjE0ZDNhZjQ5LTRjY2MtNDVkZi1iMWQyLWZiMDg1MTcwODc5MCIsInN1YiI6IkhhcmVkeXNzQHJlbnRzYWFzLmNvbSIsImVtYWlsIjoiSGFyZWR5c3NAcmVudHNhYXMuY29tIiwiZ2l2ZW5fbmFtZSI6Ik1vaGFtZWRzIEhhcmVkeXNzIiwianRpIjoiODEwNzEyNjgtNzUxZC00ZDdkLThmMDktNDk0MDI4YmZlYWI5IiwibmJmIjoxNzM2MjYxNDg0LCJleHAiOjE3MzYyNjE0OTQsImlhdCI6MTczNjI2MTQ4NH0.mRwD576CkXmCYnu3sK0b4shujpjXIGGcmqas1MjwjBRo0mdb6ZbSpONsZpfyiP7c3FRv9i1unlQ0sEU0OycQ3w',
  //     'X-OrganizationId': '00000000-0000-0000-0000-000000000001'
  //   });

  //   return this._HttpClient.get(`${this.baseUrl}get`, { headers });
  // }
}
