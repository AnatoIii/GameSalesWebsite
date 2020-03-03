import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {

  loginForm: FormGroup;
  credentials: string;
  hidePass = true;

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.createForm();
  }

  createForm() {
    this.loginForm = this.fb.group({
      email: ['', Validators.required ],
      password: ['', Validators.required ]
    });
  }

  onSubmit() {
    this.credentials = this.loginForm.value;
    console.log(this.credentials);
    this.loginForm.reset({
      email: '',
      password: ''
    });
    //this.feedbackFormDirective.resetForm();
  }
}
