import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalImageGalleryComponent } from './modal-image-gallery.component';

describe('ModalImageGalleryComponent', () => {
  let component: ModalImageGalleryComponent;
  let fixture: ComponentFixture<ModalImageGalleryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModalImageGalleryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalImageGalleryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
