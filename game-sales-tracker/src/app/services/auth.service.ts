import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { LoginFormDto } from "../models/login-form-dto";
import { RegisterFormDto } from "../models/register-form-dto";
import { TokenDto } from "../models/token-dto";
import { LIST_URI } from "./rest-api.constants";

@Injectable({
    providedIn: "root",
})
export class AuthService {

    constructor(private httpClient: HttpClient) {
    }

    public login(loginForm: FormGroup): Observable<TokenDto> {
        const controls = loginForm.controls;
        const loginFormDto: LoginFormDto = {
            email: controls.email.value,
            password: controls.password.value,
        };
        return this.httpClient.post(LIST_URI.login, loginFormDto)
            .pipe(
                map((tokenDto: TokenDto) => {
                    localStorage.setItem("ACCESS_TOKEN", tokenDto.accessToken);
                    localStorage.setItem("REFRESH_TOKEN", tokenDto.refreshToken);
                    localStorage.setItem("USER_ID", tokenDto.userId);
                    return tokenDto;
                }),
            );
    }

    // tslint:disable-next-line:typedef
    public register(registerForm: FormGroup) {
        const controls = registerForm.controls;
        const registerFormDto: RegisterFormDto = {
            email: controls.email.value,
            firstName: controls.firstName.value,
            username: controls.username.value,
            lastName: controls.lastName.value,
            password: controls.password.value,
        };
        return this.httpClient.post(LIST_URI.register, registerFormDto, { observe: "response" });
    }

    public isUserAuthorized(): boolean {
        return localStorage.getItem("ACCESS_TOKEN") !== null;
    }

    public logOut(): void {
        localStorage.clear();
    }
}
