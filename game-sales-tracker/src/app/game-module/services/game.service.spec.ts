import { TestBed } from "@angular/core/testing";
import {
  HttpClientTestingModule,
  HttpTestingController,
} from "@angular/common/http/testing";
import { GameService } from "./game.service";

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

  afterEach(() => httpTestingController.verify());
});
