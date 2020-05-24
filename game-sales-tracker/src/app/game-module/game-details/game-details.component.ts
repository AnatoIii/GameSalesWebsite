import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { GameService } from "../services/game.service";
import { IGame } from "../interfaces/game";

@Component({
  selector: "app-game-details",
  templateUrl: "./game-details.component.html",
  styleUrls: ["./game-details.component.css"],
})
export class GameDetailsComponent implements OnInit {
  game: IGame;
  sellers;
  additionalImages: string[];
  indexClickedImage: number;

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService
  ) {}

  ngOnInit(): void {
    this.sellers = [
      {
        gameName: "Sims 4 Ru/Key asd",
        sellerName: "PS store",
        price: 47,
      },
      {
        gameName: "Sims 4 Ru/yyy",
        sellerName: "Another store",
        price: 54,
      },
    ];

    const id = +this.route.snapshot.paramMap.get("id");

    this.gameService.getGameDetails(id).subscribe(
      (data: IGame) => {
        this.game = data;
        this.additionalImages = [
          "game-fish.jpg",
          this.game.image,
          this.game.image,
          "game-fish.jpg",
        ];
      },
      (error) => console.log(error)
    );
  }

  openCloseModalImageGallery(index?: number) {
    this.indexClickedImage = index;
  }

  getImagesForModal(): string[] {
    return [this.game.image, ...this.additionalImages];
  }
}
