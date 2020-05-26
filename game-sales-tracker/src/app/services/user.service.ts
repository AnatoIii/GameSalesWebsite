import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import {
    Observable,
    of,
} from "rxjs";
import {
    IUpdateUserDto,
} from "../models/update-user-dto";
import { IUser } from "../models/user";
import { LIST_URI } from "./rest-api.constants";

@Injectable({
    providedIn: "root",
})
export class UserService {

    constructor(private httpClient: HttpClient) {
    }

    public getAll(): Observable<IUser[]> {
        return this.httpClient.get<IUser[]>(LIST_URI.getAllUsers);
    }

    public getById(id: string): Observable<IUser> {
        return this.httpClient.get<IUser>(`${LIST_URI.getById}${id}`);
    }

    public updateUser(updateUserDto: IUpdateUserDto): Observable<any> {
        return this.httpClient.post<IUpdateUserDto>(LIST_URI.updateUser, updateUserDto, { observe: "response" });
    }

    public uploadPhoto(userId: string, image: File): Observable<any> {
        const imageDto = new FormData();
        imageDto.append("userId", userId);
        imageDto.append("image", image);
        return this.httpClient.post<IUpdateUserDto>(LIST_URI.uploadPhoto, imageDto, { observe: "response" });
    }
}
