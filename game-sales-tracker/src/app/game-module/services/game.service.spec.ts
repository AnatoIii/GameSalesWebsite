import { TestBed } from "@angular/core/testing";
import {
  HttpClientTestingModule,
  HttpTestingController,
} from "@angular/common/http/testing";
import { GameService } from "./game.service";
import { IGame } from "../interfaces/IGame";
import { IPlatform } from "../interfaces/IPlatform";
import { IFullGame } from "../interfaces/IFullGame";

describe("GameService", () => {
  let service: GameService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [GameService],
    });
    service = TestBed.get(GameService);
    httpTestingController = TestBed.get(HttpTestingController);
  });

  it("should be created", () => {
    expect(service).toBeTruthy();
  });

  it("should test http.get getGames()", () => {
    const data: IGame[] = [];
    service.getGames().subscribe((res) => expect(res).toBe(data));
    const req = httpTestingController.expectOne("/games");
    expect(req.request.method).toBe("GET");
    req.flush(data);
  });

  it("should test http.get getPlatforms()", () => {
    const data: IPlatform[] = [];
    service.getPlatforms().subscribe((res) => expect(res).toBe(data));
    const req = httpTestingController.expectOne("/platforms");
    expect(req.request.method).toBe("GET");
    req.flush(data);
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
  });

  afterEach(() => httpTestingController.verify());
});
