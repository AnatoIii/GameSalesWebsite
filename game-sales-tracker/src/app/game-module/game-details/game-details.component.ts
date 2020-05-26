import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { GameService } from "../services/game.service";
import { IFullGame } from "../interfaces/IFullGame";
import { CurrencySymbol } from "../interfaces/IPlatformGamePrice";

@Component({
  selector: "app-game-details",
  templateUrl: "./game-details.component.html",
  styleUrls: ["./game-details.component.css"],
})
export class GameDetailsComponent implements OnInit {
  game: IFullGame;
  indexClickedImage: number;

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService
  ) {}

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get("id");

    this.gameService.getGameDetails(id).subscribe(
      (data: IFullGame) => {
        data.Platforms.sort((a, b) => a.DiscountedPrice - b.DiscountedPrice);
        this.game = data;
      },
      (error) => console.log(error)
    );
  }

  openCloseModalImageGallery(index?: number) {
    this.indexClickedImage = index;
  }

  getConvertedPrice(price: number): string {
    return `${(price / 100).toFixed(2)} ${
      CurrencySymbol[this.game.Platforms[0].CurrencyId]
    }`;
  }

  goToExternalLink(link: string): void {
    window.open(link, "_blank");
  }
}
