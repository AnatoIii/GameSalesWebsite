import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { Observable } from "rxjs";
import {
    map,
    take,
} from "rxjs/operators";
import { LoginFormDto } from "../shared/models/login-form-dto";
import { RegisterFormDto } from "../shared/models/register-form-dto";
import { TokenDto } from "../shared/models/token-dto";
import { LIST_URI } from "./rest-api.constants";

@Injectable({
    providedIn: "root",
})
export class AuthService {

    constructor(private httpClient: HttpClient) {
    }

    public login(loginForm: FormGroup): Observable<TokenDto> {
        const loginFormDto = new LoginFormDto();
        loginFormDto.email = loginForm.controls.email.value;
        loginFormDto.password = loginForm.controls.password.value;
        return this.httpClient.post(LIST_URI.login, loginFormDto)
            .pipe(
                take(1),
                map((tokenDto: TokenDto) => {
                    localStorage.setItem("ACCESS_TOKEN", tokenDto.accessToken);
                    localStorage.setItem("REFRESH_TOKEN", tokenDto.refreshToken);
                    return tokenDto;
                }),
            );
    }

    // tslint:disable-next-line:typedef
    public register(registerForm: FormGroup) {
        const registerFormDto = new RegisterFormDto();
        registerFormDto.email = registerForm.controls.email.value;
        registerFormDto.firstName = registerForm.controls.firstName.value;
        registerFormDto.lastName = registerForm.controls.lastName.value;
        registerFormDto.password = registerForm.controls.password.value;
        return this.httpClient.post(LIST_URI.register, registerFormDto, { observe: "response" });
    }

    public isUserAuthorized(): boolean {
        return localStorage.getItem("ACCESS_TOKEN") === null;
    }
}
