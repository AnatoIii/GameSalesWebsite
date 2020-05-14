import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { IGame } from '../interfaces/game';
import { Observable } from 'rxjs';
import { IFilterRequest } from '../interfaces/filterRequest';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  baseURL = 'http://localhost:8082/';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'JWT ' + localStorage.getItem('token'),
    }),
  };

  constructor(private http: HttpClient) {}

  getGames(): Observable<IGame[]> {
    return this.http.get<IGame[]>(this.baseURL + 'api/games', this.httpOptions);
  }

  getGameGenres(): Observable<string[]> {
    return this.http.get<string[]>(
      this.baseURL + 'api/genres',
      this.httpOptions
    );
  }

  sendFilrtersParams(filtersParams: IFilterRequest) {
    console.log(filtersParams);
    let params = new HttpParams();
    Object.keys(filtersParams).forEach((key) => {
      params = params.append(key, filtersParams[key]);
    });

    this.http
      .get(this.baseURL + 'api/games', {
        headers: this.httpOptions.headers,
        params: params,
      })
      .subscribe();
  }
}
