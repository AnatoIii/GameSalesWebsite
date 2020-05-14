import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';
import { FormsModule } from '@angular/forms';

import { MainComponent } from '../main/main.component';
import { CountdownComponent } from '../countdown/countdown.component';
import { CarouselComponent } from '../carousel/carousel.component';
import { GamesFilterComponent } from '../games-filter/games-filter.component';

@NgModule({
  declarations: [
    MainComponent,
    CountdownComponent,
    CarouselComponent,
    GamesFilterComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatRadioModule,
  ],
})
export class GameModule {}
