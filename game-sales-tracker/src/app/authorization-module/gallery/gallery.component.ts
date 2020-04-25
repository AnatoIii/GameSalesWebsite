import { Component, OnInit } from '@angular/core';
import { Game } from '../../shared/models/game';
import { GameService } from '../services/game.service';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css'],
})
export class GalleryComponent implements OnInit {
  games: Game[];

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.gameService.getGames().subscribe(
      (data) => (this.games = data),
      (error) => console.log(error)
    );
  }
}
