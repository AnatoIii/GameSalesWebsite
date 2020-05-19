import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import {
    RouterModule,
    Routes,
} from "@angular/router";
import { LoginFormComponent } from "../authorization-module/login-form/login-form.component";
import { RegisterFormComponent } from "../authorization-module/register-form/register-form.component";
import { GameDetailsComponent } from "../game-module/game-details/game-details.component";
import { MainComponent } from "../game-module/main/main.component";
import { LandingPageComponent } from "../landing-page/landing-page.component";
import { ProfileComponent } from "../profile/profile.component";

const routes: Routes = [
    { path: "", component: LandingPageComponent },
    { path: "login", component: LoginFormComponent },
    { path: "register", component: RegisterFormComponent },
    { path: "profile", component: ProfileComponent },
    { path: "**", redirectTo: "/" },
    { path: "", component: MainComponent },
    { path: "login", component: LoginFormComponent },
    { path: "register", component: RegisterFormComponent },
    { path: "game/:id", component: GameDetailsComponent },
    { path: "**", redirectTo: "/" },
];

@NgModule({
    imports: [CommonModule, RouterModule.forRoot(routes)],
    exports: [RouterModule],
    declarations: [],
})
export class AppRoutingModule {
}
