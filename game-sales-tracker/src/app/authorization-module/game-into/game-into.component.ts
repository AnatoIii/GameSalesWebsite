import { Component, OnInit, Input } from "@angular/core";
import { Game } from "../../shared/models/game";
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from "@angular/animations";

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
        })
      ),
      transition("initial <=> hover", animate("1s")),
    ]),
  ],
})
export class GameIntoComponent implements OnInit {
  @Input() public game: Game;
  public blockOnHover = false;

  public ngOnInit(): void {}
}
