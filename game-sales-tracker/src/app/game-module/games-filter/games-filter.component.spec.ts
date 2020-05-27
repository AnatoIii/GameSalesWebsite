import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { GamesFilterComponent } from "./games-filter.component";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { SortType } from "../interfaces/IFilterOptions";
import { GameService } from "../services/game.service";
import { IGame } from "../interfaces/IGame";
import { IPlatform } from "../interfaces/IPlatform";

describe("GamesFilterComponent", () => {
  let component: GamesFilterComponent;
  let fixture: ComponentFixture<GamesFilterComponent>;
  let gameService;
  const expectedGames: IGame[] = [
    {
      Id: 0,
      Name: "Test",
      Description: "test",
      Images: ["image1"],
      BestPrice: {
        DiscountedPrice: 0,
        CurrencyId: 1,
      },
    },
  ];
  const expectedPlatforms: IPlatform[] = [
    {
      Id: 0,
      Name: "Test",
    },
  ];
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [GamesFilterComponent],
      imports: [HttpClientTestingModule],
      providers: [GameService],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GamesFilterComponent);
    component = fixture.componentInstance;
    gameService = jasmine.createSpyObj("GameService", {
      getGames: expectedGames,
      getPlatforms: expectedPlatforms,
    });
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

  it("should get games from GameService getGameDetails()", () => {
    component.games = gameService.getGames();
    expect(component.games).toBe(expectedGames);
  });

  it("should get platforms from GameService getPlatforms()", () => {
    component.platforms = gameService.getGames().map((x) => [false, x]);
    expect(component.platforms).toEqual(expectedGames.map((x) => [false, x]));
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
});
