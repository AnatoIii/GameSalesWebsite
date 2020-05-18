import {
    Component,
    OnInit,
} from "@angular/core";
import { Observable } from "rxjs";
import { GameService } from "src/app/services/game.service";
import { Game } from "../../shared/models/game";

@Component({
    selector: "app-gallery",
    templateUrl: "./gallery.component.html",
    styleUrls: ["./gallery.component.css"],
})
export class GalleryComponent implements OnInit {

    constructor(private gameService: GameService) {
    }

    public games$: Observable<Game[]>;

    public ngOnInit(): void {
        this.games$ = this.gameService.getAll();
    }

}
