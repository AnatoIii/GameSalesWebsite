import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule, Routes } from '@angular/router';

import { GalleryComponent } from '../gallery/gallery.component';
import { LoginFormComponent } from '../login-form/login-form.component';
import { RegisterFormComponent } from '../register-form/register-form.component';
import { GameIntoComponent } from '../game-into/game-into.component';

const matModules = [MatButtonModule, MatFormFieldModule, MatInputModule];

@NgModule({
  declarations: [
    GalleryComponent,
    LoginFormComponent,
    RegisterFormComponent,
    GameIntoComponent,
  ],
  imports: [CommonModule, ReactiveFormsModule, RouterModule, [...matModules]],
})
export class AuthModule {}
