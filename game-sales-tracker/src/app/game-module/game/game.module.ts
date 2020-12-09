import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatRadioModule } from "@angular/material/radio";
import { FormsModule } from "@angular/forms";
import { AppRoutingModule } from "../../app-routing/app-routing.module";
import { HttpClientModule } from "@angular/common/http";

import { MainComponent } from "../main/main.component";
import { CountdownComponent } from "../countdown/countdown.component";
import { CarouselComponent } from "../carousel/carousel.component";
import { GamesFilterComponent } from "../games-filter/games-filter.component";
import { PaginatorComponent } from "../paginator/paginator.component";
import { GameDetailsComponent } from "../game-details/game-details.component";
import { ModalImageGalleryComponent } from "../modal-image-gallery/modal-image-gallery.component";

@NgModule({
  declarations: [
    MainComponent,
    CountdownComponent,
    CarouselComponent,
    GamesFilterComponent,
    PaginatorComponent,
    GameDetailsComponent,
    ModalImageGalleryComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatRadioModule,
    AppRoutingModule,
    HttpClientModule,
  ],
})
export class GameModule {}
