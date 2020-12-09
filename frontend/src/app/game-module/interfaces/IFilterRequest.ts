import { IFilterOptions } from "./IFilterOptions";

export interface IFilterRequest {
  From: number;
  CountPerPage: number;
  FilterOptions: IFilterOptions;
}
