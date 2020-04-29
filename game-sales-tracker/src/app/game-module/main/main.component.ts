import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/game-module/services/game.service';
import { Game } from '../../shared/models/game';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  games: Game[];

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.gameService.getGames().subscribe(
      (data) => (this.games = data),
      (error) => console.log(error)
    );
  }
}
