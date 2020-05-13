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

@Component({
    selector: "app-login-form",
    templateUrl: "./login-form.component.html",
    styleUrls: ["./login-form.component.css"],
})
export class LoginFormComponent implements OnInit {

    constructor(private fb: FormBuilder) {
    }

    @ViewChild("form") public loginFormDirective;
    public loginForm: FormGroup;
    public credentials: string;
    public hidePass = true;

    public ngOnInit(): void {
        this.createForm();
    }

    public createForm(): void {
        this.loginForm = this.fb.group({
            email: ["", Validators.required],
            password: ["", Validators.required],
        });
    }

    public onSubmit(): void {
        this.credentials = this.loginForm.value;
        this.loginFormDirective.resetForm();
    }
}
