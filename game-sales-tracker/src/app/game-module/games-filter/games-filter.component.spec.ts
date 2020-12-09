import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { GamesFilterComponent } from "./games-filter.component";
import { HttpClientTestingModule } from "@angular/common/http/testing";

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
});
