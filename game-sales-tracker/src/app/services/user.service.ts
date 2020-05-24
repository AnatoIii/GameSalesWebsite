import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import {
    Observable,
    of,
} from "rxjs";
import {
    filter,
    map,
} from "rxjs/operators";
import { UpdateUserDto } from "../shared/models/update-user-dto";
import { User } from "../shared/models/user";
import { LIST_URI } from "./rest-api.constants";

@Injectable({
    providedIn: "root",
})
export class UserService {

    constructor(private httpClient: HttpClient) {
    }

    public getAll(): Observable<User[]> {
        return this.httpClient.get<User[]>(LIST_URI.getAllUsers);
    }

    public getById(id: string): Observable<User> {
        return this.httpClient.get<User>(`${LIST_URI.getById}${id}`);
    }

    public updateUser(updateUserDto: UpdateUserDto): Observable<any> {
        return this.httpClient.post<UpdateUserDto>(LIST_URI.updateUser, updateUserDto, { observe: "response" });
    }

    public uploadPhoto(userId: string, image: File): Observable<any> {
        const imageDto = new FormData();
        imageDto.append("userId", userId);
        imageDto.append("image", image);
        return this.httpClient.post<UpdateUserDto>(LIST_URI.uploadPhoto, imageDto, { observe: "response" });
    }
}
