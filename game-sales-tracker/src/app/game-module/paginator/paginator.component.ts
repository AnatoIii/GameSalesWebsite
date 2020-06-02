import {
  Component,
  OnInit,
  EventEmitter,
  Input,
  Output,
  SimpleChange,
} from "@angular/core";
import { Observable, Subscription } from "rxjs";

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
  gamesCountSubscription: Subscription;

  constructor() {}

  ngOnInit(): void {
    this.setPagesArray(this.gamesCount);
  }

  ngOnChanges() {
    this.setPagesArray(this.gamesCount);
  }

  setPagesArray(gamesCount: number): void {
    const pagesAmount = Math.ceil(gamesCount / this.countPerPage) || 0;
    this.pages = [...Array(pagesAmount).keys()].map((x) => x + 1);
  }
}
