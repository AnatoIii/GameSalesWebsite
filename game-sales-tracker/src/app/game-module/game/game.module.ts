import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainComponent } from '../main/main.component';
import { CountdownComponent } from '../countdown/countdown.component';

@NgModule({
  declarations: [MainComponent, CountdownComponent],
  imports: [CommonModule],
})
export class GameModule {}
