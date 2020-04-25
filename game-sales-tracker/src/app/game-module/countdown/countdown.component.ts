import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/internal/operators';
import { interval } from 'rxjs';

@Component({
  selector: 'app-countdown',
  templateUrl: './countdown.component.html',
  styleUrls: ['./countdown.component.css'],
})
export class CountdownComponent implements OnInit {
  timetoSale: {
    days: number;
    hours: number;
    minutes: number;
    seconds: number;
  };

  constructor() {}

  ngOnInit(): void {
    const saleDate = new Date('December 17, 2020 03:24:00');

    interval(1000)
      .pipe(
        map((x) => {
          return Math.floor((saleDate.getTime() - new Date().getTime()) / 1000);
        })
      )
      .subscribe((x) => {
        this.timetoSale = this.dhms(x);
      });
  }

  dhms(t) {
    var days, hours, minutes, seconds;
    days = Math.floor(t / 86400);
    t -= days * 86400;
    hours = Math.floor(t / 3600) % 24;
    t -= hours * 3600;
    minutes = Math.floor(t / 60) % 60;
    t -= minutes * 60;
    seconds = t % 60;

    return { days: days, hours: hours, minutes: minutes, seconds: seconds };
  }
}
