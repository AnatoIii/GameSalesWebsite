import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GameIntoComponent } from './game-into.component';

describe('GameIntoComponent', () => {
  let component: GameIntoComponent;
  let fixture: ComponentFixture<GameIntoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GameIntoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GameIntoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
