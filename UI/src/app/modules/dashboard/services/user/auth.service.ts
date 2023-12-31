import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }
user: any=null;
  loadUser(){
    return this.http.get<any>("/api/user");
  }
  login(loginForm: any){
    return this.http.post<any>("/api/login",loginForm,{withCredentials:true});
  }
  register(){}
  logout(){}

}
