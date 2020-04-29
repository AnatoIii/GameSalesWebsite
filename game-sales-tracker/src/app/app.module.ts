import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing/app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';

import { AppComponent } from './app.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { SignInComponent } from './dialogs/sign-in/sign-in.component';
import { HeaderComponent } from './header/header.component';
import { AuthModule } from './authorization-module/auth/auth.module';
import { GameModule } from './game-module/game/game.module';

@NgModule({
  declarations: [
    AppComponent,
    SignInComponent,
    LandingPageComponent,
    HeaderComponent,
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
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [SignInComponent],
  exports: [HeaderComponent],
})
export class AppModule {}
