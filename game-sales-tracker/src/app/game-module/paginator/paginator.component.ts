import { Component, OnInit, EventEmitter, Input, Output } from "@angular/core";

@Component({
  selector: "app-paginator",
  templateUrl: "./paginator.component.html",
  styleUrls: ["./paginator.component.css"],
})
export class PaginatorComponent implements OnInit {
  @Input() currentPage: number;
  @Input() gamesCount: number;
  @Input() countPerPage: number;
  @Output() pageChange = new EventEmitter<number>();

  pages: number[];

  constructor() {}

  ngOnInit(): void {
    this.setPagesArray();
  }

  setPagesArray(): void {
    const pagesAmount = Math.ceil(this.gamesCount / this.countPerPage) || 0;
    this.pages = [...Array(pagesAmount).keys()].map((x) => x + 1);
  }
}
