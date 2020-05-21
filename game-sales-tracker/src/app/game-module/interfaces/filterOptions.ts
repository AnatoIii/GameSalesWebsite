export interface IFilterOptions {
  gameName: string;
  genres: string[];
  sortType: SortType;
}

export enum SortType {
  popularity = 'popularity',
  cheap = 'cheap',
  expensive = 'expensive',
}
