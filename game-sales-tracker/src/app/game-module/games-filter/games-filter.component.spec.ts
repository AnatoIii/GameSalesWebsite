import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { GamesFilterComponent } from "./games-filter.component";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { SortType } from "../interfaces/IFilterOptions";
import { By } from "@angular/platform-browser";

describe("GamesFilterComponent", () => {
  let component: GamesFilterComponent;
  let fixture: ComponentFixture<GamesFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [GamesFilterComponent],
      imports: [HttpClientTestingModule],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GamesFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should set initial variables OnOnit", () => {
    component.ngOnInit();
    const testFilterOptions = {
      GameName: "",
      Platforms: [],
      SortType: SortType.basePrice,
      AscendingOrder: true,
    };

    expect(component.filterOptions).toEqual(testFilterOptions);
    expect(component.pageOptions).toEqual({
      From: 0,
      CountPerPage: 10,
      FilterOptions: testFilterOptions,
    });
  });

  it("should changed filter after filterChanged()", () => {
    component.platforms = [[true, { Id: 0, Name: "Test" }]];
    component.filterChanged();
    fixture.detectChanges();
    expect(component.filterOptions.Platforms[0]).toBe(0);
    expect(component.filterOptions.Platforms.length).toBe(1);
  });

  it("should changed page options 'from' after pageChanged()", () => {
    component.pageOptions = { From: 0, CountPerPage: 10, FilterOptions: null };
    component.pageChanged(2);
    fixture.detectChanges();
    expect(component.pageOptions.From).toBe(10);
  });

  // it("should set input property 'currentPage' equal to page in <app-paginator/>", () => {
  //   component.games = [];
  //   component.page = 2;
  //   fixture.detectChanges();
  //   let element = fixture.debugElement.query(By.css("app-paginator"));
  //   expect(
  //     element.nativeElement.getAttribute("ng-reflect-current-page")
  //   ).toEqual("null");
  // });
});
