export interface IGame {
  Id: number;
  Name: string;
  Description: string;
  Images: string[];
  BestPrice: {
    DiscountedPrice: number;
    CurrencyId: number;
  };
}
