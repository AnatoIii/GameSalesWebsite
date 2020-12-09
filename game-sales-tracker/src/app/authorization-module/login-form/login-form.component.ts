import { Component, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "../services/auth.service";
import { Title } from "@angular/platform-browser";

@Component({
  selector: "app-login-form",
  templateUrl: "./login-form.component.html",
  styleUrls: ["./login-form.component.css"],
})
export class LoginFormComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private titleService: Title
  ) {}

  public loginForm: FormGroup;
  public hidePass = true;

  public ngOnInit(): void {
    this.createForm();
    this.titleService.setTitle("GaST — Login");
  }

  public createForm(): void {
    this.loginForm = this.fb.group({
      email: ["", Validators.required],
      password: ["", Validators.required],
    });
  }

  public onSubmit(): void {
    this.authService.login(this.loginForm).subscribe(
      () => this.router.navigate(["/"]),
      (error) => {
        const errorMessage = error.error.split(":")[1];
        alert(errorMessage);
      }
    );
  }
}
