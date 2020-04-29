export interface IFilterRequest {
  gameName: string;
  genres: string[];
  sortType: SortType;
}

enum SortType {
  popularity = 'popularity',
  cheap = 'cheap',
  expensive = 'expensive',
}
