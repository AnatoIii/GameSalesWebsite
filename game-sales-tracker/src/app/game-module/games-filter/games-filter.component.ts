import { Component, OnInit } from '@angular/core';
import { GameService } from '../services/game.service';
import { IGame } from '../interfaces/game';

@Component({
  selector: 'app-games-filter',
  templateUrl: './games-filter.component.html',
  styleUrls: ['./games-filter.component.css'],
})
export class GamesFilterComponent implements OnInit {
  games: IGame[];

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.gameService.getGames().subscribe(
      (data) => (this.games = data),
      (error) => console.log(error)
    );
  }
}
