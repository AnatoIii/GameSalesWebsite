import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from "../../shared/models/user";
import { LIST_URI } from "./rest-api.constants";

@Injectable({
  providedIn: "root",
})
export class UserService {
  constructor(private httpClient: HttpClient) {}

  public getAll(): Observable<User[]> {
    return this.httpClient.get<User[]>(LIST_URI.getAllUsers);
  }
}
