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
  const expectedPlatforms: IPlatform[] = [
    {
      Id: 0,
      Name: "Test",
    },
  ];
  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "JWT " + localStorage.getItem("token"),
    }),
  };
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

  it("should test http.get getBestGames()", () => {
    const data: IGame[] = [];
    service.getBestGames(10).subscribe((res) => expect(res).toBe(data));
    const req = httpTestingController.expectOne("/gamesprices/best?count=10");
    expect(req.request.method).toBe("GET");
    req.flush(data);
    httpTestingController.verify();
  });

  it("should test http.get getPlatforms()", () => {
    const data: IPlatform[] = [];
    service.getPlatforms().subscribe((res) => expect(res).toBe(data));
    const req = httpTestingController.expectOne("/platforms");
    expect(req.request.method).toBe("GET");
    req.flush(data);
    httpTestingController.verify();
  });

  it("should test http.get getGameDetails(id)", () => {
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
    service.getPlatforms().subscribe((platforms) => {
      expect(platforms).toEqual(expectedPlatforms);
      expect(platforms.length).toBe(1);
    });
  });
  //afterEach(() => httpTestingController.verify());
});
