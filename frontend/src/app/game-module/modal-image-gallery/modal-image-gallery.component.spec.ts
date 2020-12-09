import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { ModalImageGalleryComponent } from "./modal-image-gallery.component";
import { Component } from "@angular/core";

describe("ModalImageGalleryComponent", () => {
  let component: TestGameDetailsComponent;
  let fixture: ComponentFixture<TestGameDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ModalImageGalleryComponent, TestGameDetailsComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestGameDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create TestGameDetailsComponent", () => {
    expect(component).toBeTruthy();
  });

  it("should be null if indexClickedImage is undefined", () => {
    component.indexClickedImage = undefined;
    fixture.detectChanges();
    expect(fixture.nativeElement.querySelector(".gallery")).toEqual(null);
  });

  it("should render correct image", () => {
    component.indexClickedImage = 1;
    fixture.detectChanges();
    let imageName = fixture.nativeElement.querySelector("img").src.slice(-6);
    expect(imageName).toBe("image2");
  });

  it("should set indexClickedImage equal undefined when close modal", () => {
    component.openCloseModalImageGallery();
    fixture.detectChanges();
    expect(component.indexClickedImage).toEqual(undefined);
  });

  @Component({
    template: ` <app-modal-image-gallery
      [images]="images"
      [indexClickedImage]="indexClickedImage"
      *ngIf="indexClickedImage || indexClickedImage == 0"
      (closeModal)="openCloseModalImageGallery()"
    >
    </app-modal-image-gallery>`,
  })
  class TestGameDetailsComponent {
    images = ["image1", "image2"];
    indexClickedImage = 2;

    openCloseModalImageGallery(index?: number) {
      this.indexClickedImage = index;
    }
  }
});
