import {
    HTTP_INTERCEPTORS,
    HttpClientModule,
} from "@angular/common/http";
import { NgModule } from "@angular/core";
import {
    FormsModule,
    ReactiveFormsModule,
} from "@angular/forms";
import { MatDialogModule } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AppRoutingModule } from "./app-routing/app-routing.module";

import { AppComponent } from "./app.component";
import { AuthModule } from "./authorization-module/auth/auth.module";
import { SignInComponent } from "./dialogs/sign-in/sign-in.component";

import { GameModule } from "./game-module/game/game.module";
import { HeaderComponent } from "./header/header.component";
import { ApiInterceptor } from "./interceptors/api.interceptor";
import { ProfileComponent } from "./profile/profile.component";

@NgModule({
    declarations: [
        AppComponent,
        SignInComponent,
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
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: ApiInterceptor,
            multi: true,
        },
    ],
    bootstrap: [AppComponent],
    entryComponents: [SignInComponent],
    exports: [HeaderComponent],
})
export class AppModule {
}
