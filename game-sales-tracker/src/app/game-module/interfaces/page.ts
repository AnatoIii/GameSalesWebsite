import { IFilterOptions } from './filterOptions';

export interface IPageRequest {
  from: number;
  countPerPage: number;
  filterOptions: IFilterOptions;
}
