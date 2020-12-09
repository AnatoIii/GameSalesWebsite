import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { IGame } from "../interfaces/IGame";
import { IFullGame } from "../interfaces/IFullGame";
import { Observable } from "rxjs";
import { IFilterRequest } from "../interfaces/IFilterRequest";
import { IPlatform } from "../interfaces/IPlatform";
import { filter } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class GameService {
  httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "JWT " + localStorage.getItem("token"),
    }),
  };

  constructor(private http: HttpClient) {}

  getBestGames(count: number): Observable<IGame[]> {
    return this.http.get<IGame[]>(
      "/gamesprices/best/" + count,
      this.httpOptions
    );
  }

  getPlatforms(): Observable<IPlatform[]> {
    return this.http.get<IPlatform[]>("/platforms", this.httpOptions);
  }

  getGameDetails(id: number): Observable<IFullGame> {
    return this.http.get<IFullGame>("/games/" + id, this.httpOptions);
  }

  sendPageParams(
    filterParams: IFilterRequest
  ): Observable<{ count: number; games: IGame[] }> {
    let params = new HttpParams();
    let options;
    Object.keys(filterParams).forEach((key) => {
      if (typeof filterParams[key] !== "object")
        params = params.append(key, filterParams[key]);
      else options = filterParams[key];
    });

    Object.keys(options).forEach((key) => {
      if (typeof options[key] !== "object")
        params = params.append(key, options[key]);
      else {
        options[key].forEach((x, i) => {
          params = params.append(`${key}[${i}]`, `${options[key][i]}`);
        });
      }
    });

    return this.http.get<{ count: number; games: IGame[] }>("/gamesprices", {
      headers: this.httpOptions.headers,
      params: params,
    });
  }
}
