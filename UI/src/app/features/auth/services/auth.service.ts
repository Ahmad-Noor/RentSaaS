import { Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { BehaviorSubject, Observable, of } from "rxjs";
import { LoginResponse, SocialLoginResponse } from "../types/auth.types";
import { LoginInterface } from "../../../interfaces/LoginInterface";
import { RegisterInterface } from "../../../interfaces/RegisterInterface";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";
import { jwtDecode } from "jwt-decode";
import { isPlatformBrowser } from "@angular/common";
import { environment } from "../../../../environments/environment";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  baseUrl: string = environment.apiUrl + "api/Auth/";

  userData: BehaviorSubject<any> = new BehaviorSubject(null);

  constructor(private _HttpClient: HttpClient, private _router: Router, @Inject(PLATFORM_ID) private Checkplatform:object) {


    if(isPlatformBrowser(this.Checkplatform))
      {
        if(localStorage.getItem("Token"))
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
    localStorage.removeItem("token");
    localStorage.removeItem("orgnaizationId");
    this._router.navigate(["/login"]);
  }

  SaveData(): void {
    this.userData.next(
      jwtDecode(JSON.stringify(localStorage.getItem("token")))
    );
    console.log(this.userData);
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
