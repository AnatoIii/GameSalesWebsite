import { Component, Input, Output, EventEmitter } from "@angular/core";

@Component({
  selector: "app-modal-image-gallery",
  templateUrl: "./modal-image-gallery.component.html",
  styleUrls: ["./modal-image-gallery.component.css"],
})
export class ModalImageGalleryComponent {
  @Input() images: string[];
  @Input() indexClickedImage: number;
  @Output() closeModal = new EventEmitter<any>();

  constructor() {}

  changeImage(number: number) {
    if (this.indexClickedImage == 0 && number == -1)
      this.indexClickedImage = this.images.length - 1;
    else if (this.indexClickedImage == this.images.length - 1 && number == 1)
      this.indexClickedImage = 0;
    else this.indexClickedImage += number;
  }
}
