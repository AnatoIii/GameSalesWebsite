import { Component, OnInit } from "@angular/core";
import { IGame } from "../../game-module/interfaces/IGame";
import { GameService } from "src/app/game-module/services/game.service";

@Component({
  selector: "app-gallery",
  templateUrl: "./gallery.component.html",
  styleUrls: ["./gallery.component.css"],
})
export class GalleryComponent implements OnInit {
  games: IGame[];

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.gameService.getBestGames(9).subscribe(
      (data) => (this.games = data),
      (error) => console.log(error)
    );
  }
}
