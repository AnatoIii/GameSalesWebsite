import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { IGame } from "../interfaces/IGame";
import { IFullGame } from "../interfaces/IFullGame";
import { Observable } from "rxjs";
import { IFilterRequest } from "../interfaces/IFilterRequest";
import { IPlatform } from "../interfaces/IPlatform";

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
      "/gamesprices/best?count=" + count,
      this.httpOptions
    );
  }

  getPlatforms(): Observable<IPlatform[]> {
    return this.http.get<IPlatform[]>("/platforms", this.httpOptions);
  }

  getGameDetails(id: number): Observable<IFullGame> {
    return this.http.get<IFullGame>("/games/" + id, this.httpOptions);
  }

  sendPageParams(filterParams: IFilterRequest): Observable<IGame[]> {
    let params = new HttpParams();
    Object.keys(filterParams).forEach((key) => {
      const paramsValue = filterParams[key];
      if (typeof paramsValue !== "object")
        params = params.append(key, paramsValue);
      if (typeof paramsValue === "object") {
        Object.keys(paramsValue).forEach((keyInKey) => {
          params = params.append(keyInKey, paramsValue[keyInKey]);
        });
      }
    });

    return this.http.get<IGame[]>("/games", {
      headers: this.httpOptions.headers,
      params: params,
    });
  }
}
