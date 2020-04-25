import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  currentUser: User;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    // localStorage.setItem(
    //   'token',
    //   'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MCwidXNlcm5hbWUiOiJVc2VyIDEiLCJlbWFpbCI6InFAcSIsInBhc3N3b3JkIjoiMSIsImltYWdlIjoiZ2FtZS1maXNoLmpwZyIsInJvbGUiOiJ1c2VyIiwiaWF0IjoxNTg3NzM1OTA1fQ.fv_SN2y-ggt7Zbwxihqauaw2K-AWXe1_JL0EJ7zbO6A'
    // );
    this.userService.getProfileData().subscribe(
      (data) => (this.currentUser = data),
      (error) => console.log(error)
    );
  }

  saveUser() {
    console.log(this.currentUser);
  }
}
