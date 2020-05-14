export interface IFilterRequest {
  gameName: string;
  genres: string[];
  sortType: SortType;
}

export enum SortType {
  popularity = 'popularity',
  cheap = 'cheap',
  expensive = 'expensive',
}
