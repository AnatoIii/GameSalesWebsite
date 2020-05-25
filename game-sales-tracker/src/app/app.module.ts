import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AppRoutingModule } from "./app-routing/app-routing.module";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { MatDialogModule } from "@angular/material/dialog";

import { AppComponent } from "./app.component";
import { AuthModule } from "./authorization-module/auth/auth.module";
import { ApiInterceptor } from "./interceptors/api.interceptor";
import { HeaderComponent } from "./header/header.component";

import { GameModule } from "./game-module/game/game.module";

@NgModule({
  declarations: [AppComponent, HeaderComponent],
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
export class AppModule {}
