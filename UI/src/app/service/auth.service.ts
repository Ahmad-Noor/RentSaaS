import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs'; 
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl: string = environment.apiUrl + '/api/user';
  currentUser: BehaviorSubject<any> = new BehaviorSubject(null);
  jwtHelperService = new JwtHelperService();
  private userPayload: any;

  constructor(private http: HttpClient, private router: Router) {
  
  }

  singIn(loginInfo: string[]) {
    return this.http.post<any>(this.apiUrl + '/login', {
      UserName: loginInfo[0],
      Password: loginInfo[1]
    });
  }
  singOut() {
    localStorage.clear();
    this.router.navigate(['login'])
  }

  storeToken(token: string) {
    localStorage.setItem('token', token);
    this.loadCurrentUser();
    this.userPayload = this.decodeToken();
    console.log('this.userPayload ' +token );
    console.log('this.userPayload ' +this.userPayload );
  }

  getToken() {
    return localStorage.getItem('token');
  }
  decodeToken() {
    const token = this.getToken();
    return this.jwtHelperService.decodeToken(token!);
  }
  loadCurrentUser() {
    const token = this.getToken();
    const userInfo = token != null ? this.jwtHelperService.decodeToken(token) : null;
    this.currentUser.next(userInfo);
  }
  isLoggedin(): boolean {
    return this.getToken() != null;
  }
  getUserId() {
    if (this.userPayload)
      return this.userPayload.nameid;
  }
  getFullNameFromToken() {
    if (this.userPayload)
      return this.userPayload.given_name;
  }
  getRoleFromToken() {
    if (this.userPayload)
      return this.userPayload.role;
  }

}
