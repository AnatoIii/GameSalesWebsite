import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { CountdownComponent } from "./countdown.component";

describe("CountdownComponent", () => {
  let component: CountdownComponent;
  let fixture: ComponentFixture<CountdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CountdownComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should be returned correct object from dhms()", () => {
    const tesdDate1 = new Date("December 17, 2020 03:24:00");
    const tesdDate2 = new Date("December 16, 2020 03:24:00");

    const output = component.dhms(
      Math.floor((tesdDate1.getTime() - tesdDate2.getTime()) / 1000)
    );
    expect(output.days).toBe(1);
    expect(output.hours).toBe(0);
  });
});
