import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs'; 
import { AuthService } from './auth.service';
import { Country } from '../models/country.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class CountriesService {
  apiUrl: string = environment.apiUrl + '/api/country';

  constructor(private http: HttpClient, private auth: AuthService) { }

  getAllCountries(): Observable<Country[]> {
    return this.http.get<Country[]>(this.apiUrl);
  }

  getCountryById(id: number): Observable<Country> {
    return this.http.get<Country>(`${this.apiUrl}/${id}`);
  }

  addCountry(data: Country): Observable<Country> {
    console.log(data);
    return this.http.post<Country>(this.apiUrl, data);
  }

  updateCountry(id: string, data: Country): Observable<Country> {
    // data.updatedBy = this.auth.getUserId();
    return this.http.put<Country>(this.apiUrl, data);
  }

  deleteCountry(id: string): Observable<Country> {
    return this.http.delete<Country>(`${this.apiUrl}/${id}`);
  }

}
