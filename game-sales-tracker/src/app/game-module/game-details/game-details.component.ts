import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { GameService } from '../services/game.service';
import { IGame } from '../interfaces/game';

@Component({
  selector: 'app-game-details',
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.css'],
})
export class GameDetailsComponent implements OnInit {
  game: IGame;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private gameService: GameService
  ) {}

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id');

    this.gameService.getGameDetails(id).subscribe(
      (data: IGame) => {
        this.game = data;
        console.log(this.game);
      },
      (error) => console.log(error)
    );
  }
}
