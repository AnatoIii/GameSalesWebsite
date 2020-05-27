import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { MainComponent } from "./main.component";

describe("MainComponent", () => {
  let component: MainComponent;
  let fixture: ComponentFixture<MainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [MainComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should find anchor", () => {
    const anchor = fixture.nativeElement
      .querySelector("a.more-games")
      .href.slice(-9);
    expect(fixture.nativeElement.querySelector(anchor)).toBeTruthy();
  });
});
