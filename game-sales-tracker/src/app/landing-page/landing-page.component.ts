import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SignInComponent } from '../dialogs/sign-in/sign-in.component';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {

  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDialogSignIn(): void {
    const dialogRef = this.dialog.open(SignInComponent, {
      width: '400px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result =>
      console.log(result));
  }

}
