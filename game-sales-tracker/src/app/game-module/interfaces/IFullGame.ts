import { IPlatformGamePrice } from "./IPlatformGamePrice";

export interface IFullGame {
  Id: number;
  Name: string;
  Description: string;
  Images: string[];
  Platforms: IPlatformGamePrice[];
}
