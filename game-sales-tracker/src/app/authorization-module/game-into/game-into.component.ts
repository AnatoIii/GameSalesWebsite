import { Component, OnInit, Input } from '@angular/core';
import { Game } from 'src/app/shared/models/game';
import { baseURL } from '../../shared/baseURL';

@Component({
  selector: 'app-game-into',
  templateUrl: './game-into.component.html',
  styleUrls: ['./game-into.component.css'],
})
export class GameIntoComponent implements OnInit {
  @Input() game: Game;

  constructor() {}

  ngOnInit(): void {}
}
