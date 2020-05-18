import {
    HttpClient,
    HttpHeaders,
} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Game } from "../shared/models/game";
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

    public getAll(): Observable<Game[]> {
        return this.httpClient.get<Game[]>(LIST_URI.getAllUsers);
    }
}
