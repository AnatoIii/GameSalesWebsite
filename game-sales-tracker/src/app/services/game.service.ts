import {
    HttpClient,
    HttpHeaders,
} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IGame } from "../game-module/interfaces/game";
import { LIST_URI } from "./rest-api.constants";

@Injectable({
    providedIn: "root",
})
export class GameService {

    constructor(private httpClient: HttpClient) {
    }

    // TODO: add interceptor for httpHeader
    public httpOptions = {
        headers: new HttpHeaders({
            "Content-Type": "application/json",
            "Authorization": "JWT " + localStorage.getItem("token"),
        }),
    };

    public getAll(): Observable<IGame[]> {
        return this.httpClient.get<IGame[]>(LIST_URI.getAllUsers);
    }
}
