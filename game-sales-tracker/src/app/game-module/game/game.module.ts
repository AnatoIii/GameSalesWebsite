import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainComponent } from '../main/main.component';
import { CountdownComponent } from '../countdown/countdown.component';
import { CarouselComponent } from '../carousel/carousel.component';

@NgModule({
  declarations: [MainComponent, CountdownComponent, CarouselComponent],
  imports: [CommonModule],
})
export class GameModule {}
