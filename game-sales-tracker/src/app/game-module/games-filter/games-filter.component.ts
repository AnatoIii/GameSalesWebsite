import { Component, OnInit } from '@angular/core';
import { GameService } from '../services/game.service';
import { IGame } from '../interfaces/game';
import { IFilterOptions, SortType } from '../interfaces/filterOptions';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/internal/operators';
import { IPageRequest } from '../interfaces/page';

@Component({
  selector: 'app-games-filter',
  templateUrl: './games-filter.component.html',
  styleUrls: ['./games-filter.component.css'],
})
export class GamesFilterComponent implements OnInit {
  games: IGame[];
  gameGenres: [boolean, string][];
  filterOptions: IFilterOptions;
  filterChangedSubject: Subject<IFilterOptions> = new Subject<IFilterOptions>();
  pageOptions: IPageRequest;
  gamesCount: number;
  page: number = 1;

  constructor(private gameService: GameService) {
    this.filterChangedSubject.pipe(debounceTime(1000)).subscribe(() => {
      this.gameService.sendPageParams(this.pageOptions).subscribe((data) => {
        this.gamesCount = data.count;
        //this.games = data.games;
      });
    });
  }

  ngOnInit(): void {
    this.filterOptions = {
      gameName: '',
      genres: [],
      sortType: SortType.popularity,
    };

    this.pageOptions = {
      from: 0,
      countPerPage: 10,
      filterOptions: this.filterOptions,
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

  filterChanged() {
    this.filterOptions.genres = this.gameGenres
      .filter((x) => x[0])
      .map((y) => y[1]);
    this.filterChangedSubject.next(this.filterOptions);
  }

  pageChanged(pageCount: number) {
    this.page = pageCount;
    this.pageOptions.from = (pageCount - 1) * this.pageOptions.countPerPage;
    this.filterChangedSubject.next(this.filterOptions);
  }
}
