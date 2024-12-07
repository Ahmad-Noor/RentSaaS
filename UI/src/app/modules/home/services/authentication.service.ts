import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Login } from '../models/login';
import { Register } from '../models/register';
import { Observable, observeOn } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { JwtAuth } from '../models/jwtAuth';
import { RegisterResponse } from '../models/registerResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
registerUrl='AuthManagement/Register';
loginUrl='AuthManagement/login';
getAllUsersUrl = 'users/GetAll';

  constructor(private http:HttpClient) { }

  public register(user:Register):Observable<RegisterResponse>{
 return this.http.post<RegisterResponse>(`${environment.apiURL}/${this.registerUrl}`,user);
  }

  public login(user:Register):Observable<RegisterResponse>{
    return this.http.post<RegisterResponse>(`${environment.apiURL}/${this.registerUrl}`,user);
     }
     public getAllUsers():Observable<any>{
      return this.http.get<any>(`${environment.apiURL}/${this.getAllUsersUrl}`);
       }
  
}
