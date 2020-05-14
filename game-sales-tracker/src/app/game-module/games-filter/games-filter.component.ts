import { Component, OnInit } from '@angular/core';
import { GameService } from '../services/game.service';
import { IGame } from '../interfaces/game';
import { IFilterRequest, SortType } from '../interfaces/filterRequest';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/internal/operators';

@Component({
  selector: 'app-games-filter',
  templateUrl: './games-filter.component.html',
  styleUrls: ['./games-filter.component.css'],
})
export class GamesFilterComponent implements OnInit {
  games: IGame[];
  gameGenres: [boolean, string][];
  filterOptions: IFilterRequest;
  filterChanged: Subject<IFilterRequest> = new Subject<IFilterRequest>();

  constructor(private gameService: GameService) {
    this.filterChanged
      .pipe(debounceTime(500))
      .subscribe((filter: IFilterRequest) => {
        this.gameService.sendFilrtersParams(filter);
      });
  }

  ngOnInit(): void {
    this.filterOptions = {
      gameName: '',
      genres: [],
      sortType: SortType.popularity,
    };

    this.gameService.getGames().subscribe(
      (data) => (this.games = data),
      (error) => console.log(error)
    );

    this.gameService.getGameGenres().subscribe(
      (data) => {
        this.gameGenres = data.map((x) => [false, x]);
      },
      (error) => console.log(error)
    );
  }

  filterReqChanged() {
    this.filterOptions.genres = this.gameGenres
      .filter((x) => x[0])
      .map((y) => y[1]);
    this.filterChanged.next(this.filterOptions);
  }
}
