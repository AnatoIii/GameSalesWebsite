import {
    Component,
    OnInit,
    ViewChild,
} from "@angular/core";
import {
    FormBuilder,
    FormGroup,
    Validators,
} from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "../../services/auth.service";
import { passwordsMatch } from "../../shared/validators/password-match.validator";
import { PasswordValidate } from "../../shared/validators/password.validator";

@Component({
    selector: "app-register-form",
    templateUrl: "./register-form.component.html",
    styleUrls: ["./register-form.component.css"],
})
export class RegisterFormComponent implements OnInit {

    constructor(private formBuilder: FormBuilder,
                private authService: AuthService,
                private router: Router) {
    }

    @ViewChild("form") public registerFormDirective;
    public _registerForm: FormGroup;
    public _hidePass = true;
    public _hideConfirmPass = true;

    public ngOnInit(): void {
        this.createForm();
    }

    public createForm(): void {
        this._registerForm = this.formBuilder.group(
            {
                firstName: ["",
                    [
                        Validators.minLength(3),
                        Validators.maxLength(30),
                    ],
                ],
                lastName: ["",
                    [
                        Validators.minLength(3),
                        Validators.maxLength(30),
                    ],
                ],
                email: ["",
                    [
                        Validators.required,
                        Validators.email,
                    ],
                ],
                password: ["",
                    [
                        PasswordValidate,
                    ],
                ],
                confirmPass: ["",
                    [Validators.required],
                ],
            },
            { validator: passwordsMatch("password", "confirmPass") },
        );
    }

    public onSubmit(): void {
        const credentials = this._registerForm.value;
        console.log(credentials);
        this.authService.register(this._registerForm)
            .subscribe(
                () => this.router.navigate(["/login"]),
                error => {
                    const errorMessage = error.error.split(":")[1];
                    alert(errorMessage);
                },
            );
        this.registerFormDirective.resetForm();
    }
}
