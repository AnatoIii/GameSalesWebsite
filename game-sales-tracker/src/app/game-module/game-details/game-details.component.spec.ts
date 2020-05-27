import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { GameDetailsComponent } from "./game-details.component";
import { RouterTestingModule } from "@angular/router/testing";
import { HttpClientTestingModule } from "@angular/common/http/testing";

describe("GameDetailsComponent", () => {
  let component: GameDetailsComponent;
  let fixture: ComponentFixture<GameDetailsComponent>;

  const expectedGame = {
    Id: 0,
    Name: "Test",
    Description: "test",
    Images: ["image1"],
    Platforms: [
      {
        Platform: {
          Id: 0,
          Name: "Test",
        },
        BasePrice: 0,
        DiscountedPrice: 0,
        CurrencyId: 1,
        GameURL: "test",
      },
    ],
  };

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [GameDetailsComponent],
      imports: [RouterTestingModule, HttpClientTestingModule],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GameDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should be null if game is undefibed", () => {
    component.game = undefined;
    expect(fixture.nativeElement.querySelector(".main")).toBe(null);
  });

  it("should have game name in p.name", () => {
    component.game = expectedGame;
    fixture.detectChanges();
    expect(
      fixture.nativeElement.querySelector(".price-block>.name").innerHTML
    ).toBe(expectedGame.Name);
  });

  it("should have main image from game.Images[]", () => {
    component.game = expectedGame;
    fixture.detectChanges();
    expect(
      fixture.nativeElement.querySelector(".main-image").src.slice(-6)
    ).toBe(expectedGame.Images[0]);
  });

  it("should have game.Images.length-1 additional images in gallery", () => {
    component.game = expectedGame;
    fixture.detectChanges();
    expect(
      fixture.nativeElement.querySelectorAll(".other-images img").length
    ).toBe(expectedGame.Images.length - 1);
  });
});
