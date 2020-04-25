import { Injectable } from '@angular/core';
import { baseURL } from '../../shared/baseURL';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../../shared/models/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private currentUser: User;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'JWT ' + localStorage.getItem('token'),
    }),
  };

  constructor(private http: HttpClient) {}

  getProfileData(): Observable<User> {
    return this.http.get<User>(baseURL + 'api/profile', this.httpOptions);
  }
}
