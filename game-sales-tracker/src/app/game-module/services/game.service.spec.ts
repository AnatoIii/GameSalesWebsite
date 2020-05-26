import { TestBed } from "@angular/core/testing";
import {
  HttpClientTestingModule,
  HttpTestingController,
} from "@angular/common/http/testing";
import { GameService } from "./game.service";
import { IGame } from "../interfaces/IGame";

describe("GameService", () => {
  let service: GameService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [GameService],
    });
    httpTestingController = TestBed.get(HttpTestingController);
    TestBed.configureTestingModule({});
    service = TestBed.inject(GameService);
  });

  // it("should be created", () => {
  //   expect(service).toBeTruthy();
  // });

  // it("can test HttpClient.get", () => {
  //   //const data = <ArrayLike<IGame>>;
  //   //service.getGames().subscribe(response => expect(response).toBe(data))
  //   const req = httpTestingController.expectOne("/api/games");
  //   expect(req.request.method).toBe("GET");
  //   //req.flush(data)
  // });

  afterEach(() => httpTestingController.verify());
});
