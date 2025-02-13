import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';  
import { Address } from '../models/address.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class AddressService {
  apiUrl: string = environment.apiUrl + '/api/address';

  constructor(private http: HttpClient) { }

  getAllAddress(): Observable<Address[]> {
    return this.http.get<Address[]>(this.apiUrl);
  }

  getAddressById(id: number): Observable<Address> {
    return this.http.get<Address>(`${this.apiUrl}/${id}`);
  }

  addAddress(data: Address): Observable<Address> {
    console.log(data);
    return this.http.post<Address>(this.apiUrl, data);
  }

  updateAddress(id: string, data: Address): Observable<Address> { 
    return this.http.put<Address>(this.apiUrl, data);
  }

  deleteAddress(id: string): Observable<Address> {
    return this.http.delete<Address>(`${this.apiUrl}/${id}`);
  }

}
