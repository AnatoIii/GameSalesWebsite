import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { PaginatorComponent } from "./paginator.component";

describe("PaginatorComponent", () => {
  let component: PaginatorComponent;
  let fixture: ComponentFixture<PaginatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [PaginatorComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaginatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should have 3 pages  if games count is 12, 5 games per page", () => {
    component.gamesCount = 12;
    component.countPerPage = 5;
    component.setPagesArray();
    fixture.detectChanges();
    expect(component.pages.length).toBe(3);
  });

  it("should have array [1,2] if pagesAmount is 2", () => {
    component.gamesCount = 12;
    component.countPerPage = 7;
    component.setPagesArray();
    fixture.detectChanges();
    expect(component.pages).toEqual([1, 2]);
  });

  it("should previous button be disapled if current page is 1", () => {
    component.currentPage = 1;
    fixture.detectChanges();
    expect(
      fixture.nativeElement.querySelector(".pagination>button").disabled
    ).toBe(true);
  });
});
