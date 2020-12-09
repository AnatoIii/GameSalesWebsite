import {
    HTTP_INTERCEPTORS,
    HttpClientModule,
} from "@angular/common/http";
import {
    FormsModule,
    ReactiveFormsModule,
} from "@angular/forms";
import { MatDialogModule } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { JwtModule } from "@auth0/angular-jwt";
import { AppRoutingModule } from "./app-routing/app-routing.module";
import { AppComponent } from "./app.component";
import { AuthModule } from "./authorization-module/auth/auth.module";
import { GameModule } from "./game-module/game/game.module";
import { HeaderComponent } from "./header/header.component";
import { ApiInterceptor } from "./interceptors/api.interceptor";
import { ProfileComponent } from "./profile/profile.component";

export function getToken(): string {
    return localStorage.getItem("ACCESS_TOKEN");
}

@NgModule({
    declarations: [
        AppComponent,
        HeaderComponent,
        ProfileComponent,
    ],
    imports: [
        AuthModule,
        GameModule,
        AppRoutingModule,
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        JwtModule.forRoot({
            config: {
                tokenGetter: getToken,
            },
        }),
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: ApiInterceptor,
            multi: true,
        },
    ],
    bootstrap: [AppComponent],
    entryComponents: [],
    exports: [HeaderComponent],
})
export class AppModule { }
