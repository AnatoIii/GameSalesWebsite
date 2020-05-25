import { IPlatform } from "./IPlatform";

export interface IFilterOptions {
  GameName: string;
  Platforms: IPlatform[];
  SortType: SortType;
}

export enum SortType {
  popularity = "popularity",
  cheap = "cheap",
  expensive = "expensive",
}
