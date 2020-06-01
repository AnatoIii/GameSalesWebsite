export interface IFilterOptions {
  GameName: string;
  Platforms: number[];
  SortType: SortType;
  AscendingOrder: boolean;
}

export enum SortType {
  basePrice = "basePrice",
  discountedPrice = "discountedPrice",
  discount = "discount",
}
