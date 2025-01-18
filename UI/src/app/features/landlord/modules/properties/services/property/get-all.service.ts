import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: "root",
})
export class GetAllService {
  constructor(private _httpClient: HttpClient) {}

  getAllProperties() {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'X-OrganizationId': '00000000-0000-0000-0000-000000000001',
      'Authorization': 'Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjE0ZDNhZjQ5LTRjY2MtNDVkZi1iMWQyLWZiMDg1MTcwODc5MCIsInN1YiI6IkhhcmVkeXNzQHJlbnRzYWFzLmNvbSIsImVtYWlsIjoiSGFyZWR5c3NAcmVudHNhYXMuY29tIiwiZ2l2ZW5fbmFtZSI6Ik1vaGFtZWRzIEhhcmVkeXNzIiwianRpIjoiODEwNzEyNjgtNzUxZC00ZDdkLThmMDktNDk0MDI4YmZlYWI5IiwibmJmIjoxNzM2MjYxNDg0LCJleHAiOjE3MzYyNjE0OTQsImlhdCI6MTczNjI2MTQ4NH0.mRwD576CkXmCYnu3sK0b4shujpjXIGGcmqas1MjwjBRo0mdb6ZbSpONsZpfyiP7c3FRv9i1unlQ0sEU0OycQ3w'
    });

    return this._httpClient.get(
      "https://localhost:7164/api/Property", 
      { headers }
    );
  }
}
