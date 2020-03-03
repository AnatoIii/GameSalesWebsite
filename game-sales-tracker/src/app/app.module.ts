import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 
import { AppRoutingModule } from './app-routing/app-routing.module'
// import { MatFormFieldModule } from '@angular/material/form-field';
// import { MatInputModule } from '@angular/material/input';
// import { MatDialogModule } from '@angular/material/dialog';

import { AppComponent } from './app.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { SignInComponent } from './dialogs/sign-in/sign-in.component';
import { HeaderComponent } from './shared/header/header.component';
import { AuthModule } from './authorization-module/auth/auth.module';

@NgModule({
  declarations: [
    AppComponent,
    SignInComponent,
    LandingPageComponent,
    HeaderComponent
  ],
  imports: [
    AuthModule,
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule
    // MatDialogModule,
    // MatFormFieldModule,
    // MatInputModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [
    SignInComponent
  ],
  exports: [
    HeaderComponent
  ]
})
export class AppModule { }
