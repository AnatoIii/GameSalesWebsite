import { IPlatform } from "./IPlatform";

export interface IGame {
  Id: number;
  Name: string;
  Description: string;
  Image: string;
  BestPrice: number;
  CurrencyId: number;
  Platforms: IPlatform[];
}
