import { Component, OnInit } from '@angular/core';
import { GAMES } from '../../shared/data/games';
import { Game } from '../../shared/data/game';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {

  games: Game[] = GAMES;

  constructor() { }

  ngOnInit(): void {
  }

}
