import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { IGame } from "../interfaces/game";
import { Observable } from "rxjs";
import { IPageRequest } from "../interfaces/page";

@Injectable({
  providedIn: "root",
})
export class GameService {
  baseURL = "http://localhost:8082/";

  httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "JWT " + localStorage.getItem("token"),
    }),
  };

  constructor(private http: HttpClient) {}

  getGames(): Observable<IGame[]> {
    return this.http.get<IGame[]>("/games", this.httpOptions);
  }

  getGameGenres(): Observable<string[]> {
    return this.http.get<string[]>("/genres", this.httpOptions);
  }

  getGameDetails(id: number): Observable<IGame> {
    return this.http.get<IGame>("/games/" + id, this.httpOptions);
  }

  sendPageParams(
    pageParams: IPageRequest
  ): Observable<{ count: number; games: IGame[] }> {
    let params = new HttpParams();
    Object.keys(pageParams).forEach((key) => {
      const paramsValue = pageParams[key];
      if (typeof paramsValue !== "object")
        params = params.append(key, paramsValue);
      if (typeof paramsValue === "object") {
        Object.keys(paramsValue).forEach((keyInKey) => {
          params = params.append(keyInKey, paramsValue[keyInKey]);
        });
      }
    });

    return this.http.get<{ count: number; games: IGame[] }>("/games", {
      headers: this.httpOptions.headers,
      params: params,
    });
  }
}