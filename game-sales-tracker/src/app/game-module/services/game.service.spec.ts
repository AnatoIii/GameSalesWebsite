import { TestBed, async, inject } from "@angular/core/testing";
import {
  HttpClientTestingModule,
  HttpTestingController,
} from "@angular/common/http/testing";
import { GameService } from "./game.service";
import { IGame } from "../interfaces/IGame";
import { IPlatform } from "../interfaces/IPlatform";
import { IFullGame } from "../interfaces/IFullGame";
import {
  HttpClientModule,
  HttpHeaders,
  HttpClient,
} from "@angular/common/http";

describe("GameService", () => {
  let service: GameService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, HttpClientModule],
      providers: [GameService],
    });
    service = TestBed.get(GameService);
    httpTestingController = TestBed.get(HttpTestingController);
  });

  it("should be created", () => {
    expect(service).toBeTruthy();
  });

  it("should test getBestGames() with HttpTestingController", () => {
    const data: IGame[] = [];
    service.getBestGames(10).subscribe((res) => expect(res).toBe(data));
    const req = httpTestingController.expectOne("/gamesprices/best?count=10");
    expect(req.request.method).toBe("GET");
    req.flush(data);
    httpTestingController.verify();
  });

  it("should test getPlatforms() with HttpTestingController", () => {
    const data: IPlatform[] = [];
    service.getPlatforms().subscribe((res) => expect(res).toBe(data));
    const req = httpTestingController.expectOne("/platforms");
    expect(req.request.method).toBe("GET");
    req.flush(data);
    httpTestingController.verify();
  });

  it("should test getGameDetails(id) with HttpTestingController", () => {
    const data: IFullGame = {
      Id: 0,
      Name: "Test",
      Description: "Test",
      Images: [],
      Platforms: [],
    };
    service.getGameDetails(0).subscribe((res) => expect(res).toBe(data));
    const req = httpTestingController.expectOne("/games/0");
    expect(req.request.method).toBe("GET");
    req.flush(data);
    httpTestingController.verify();
  });

  it("should get platforms", async () => {
    const expectedPlatforms: IPlatform[] = [
      {
        Id: 0,
        Name: "Test",
      },
    ];
    service.getPlatforms().subscribe((platforms) => {
      expect(platforms).toEqual(expectedPlatforms);
      expect(platforms.length).toBe(1);
    });
  });

  it("should get 10 best games", async () => {
    service.getBestGames(10).subscribe((games) => {
      expect(games.length).toBe(10);
    });
  });
});
