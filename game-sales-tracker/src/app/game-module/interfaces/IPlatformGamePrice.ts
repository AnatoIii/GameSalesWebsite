import { IPlatform } from "./IPlatform";

export interface IPlatformGamePrice {
  Platform: IPlatform;
  BasePrice: number;
  DiscountedPrice: number;
  CurrencyId: number;
  GameURL: string;
}

export enum CurrencySymbol {
  "\u20B4" = 1,
  "\u0024",
  "\u20BD",
}
