import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from '../landing-page/landing-page.component';
import { RegisterFormComponent } from '../authorization-module/register-form/register-form.component';
import { LoginFormComponent } from '../authorization-module/login-form/login-form.component';
import { MainComponent } from '../game-module/main/main.component';
import { GameDetailsComponent } from '../game-module/game-details/game-details.component';

const routes: Routes = [
  { path: '', component: LandingPageComponent },
  { path: 'login', component: LoginFormComponent },
  { path: 'register', component: RegisterFormComponent },
  { path: 'game/:id', component: GameDetailsComponent },
  { path: 'main', component: MainComponent },
  { path: '**', redirectTo: '/' },
];

@NgModule({
  imports: [CommonModule, RouterModule.forRoot(routes)],
  exports: [RouterModule],
  declarations: [],
})
export class AppRoutingModule {}
