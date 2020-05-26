import { Component, Input } from "@angular/core";
import { IGame } from "../../game-module/interfaces/IGame";
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from "@angular/animations";
import { CurrencySymbol } from "src/app/game-module/interfaces/IPlatformGamePrice";

@Component({
  selector: "app-game-into",
  templateUrl: "./game-into.component.html",
  styleUrls: ["./game-into.component.css"],
  animations: [
    trigger("onHover", [
      state(
        "initial",
        style({
          padding: "10px",
          height: "30%",
        })
      ),
      state(
        "hover",
        style({
          padding: "40px 10px",
          height: "100%",
          cursor: "pointer",
        })
      ),
      transition("* => *", animate("0.5s ease-in-out")),
    ]),
    trigger("gameDescription", [
      state(
        "initial",
        style({
          opacity: "0",
        })
      ),
      state(
        "hover",
        style({
          opacity: "1",
          cursor: "pointer",
        })
      ),
      transition("initial <=> hover", animate("1s")),
    ]),
  ],
})
export class GameIntoComponent {
  @Input() game: IGame;
  blockOnHover = false;

  constructor() {}

  getConvertedPrice(game: IGame): string {
    return `${(game.BestPrice.DiscountedPrice / 100).toFixed(2)} ${
      CurrencySymbol[game.BestPrice.CurrencyId]
    }`;
  }
}
