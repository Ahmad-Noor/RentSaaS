import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MasterService {
  public onProjectChange = new Subject();
  public onTicketCreate = new Subject();

  constructor(private http: HttpClient) {}
  get(url: string): Observable<any> {
    return this.http.get(url);
  }
}
