import { Injectable } from '@angular/core';
import { baseURL } from '../../shared/baseURL';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Game } from '../../shared/models/game';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private http: HttpClient) {}

  getGames(): Observable<Game[]> {
    return this.http.get<Game[]>(baseURL + 'api/games', this.httpOptions);
  }
}
