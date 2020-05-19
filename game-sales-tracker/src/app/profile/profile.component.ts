import {
    Component,
    OnInit,
} from "@angular/core";
import {
    FormBuilder,
    FormGroup,
    Validators,
} from "@angular/forms";
import { Observable } from "rxjs";
import { UserService } from "../services/user.service";
import { User } from "../shared/models/user";
import { passwordsMatch } from "../shared/validators/password-match.validator";
import { PasswordValidate } from "../shared/validators/password.validator";

@Component({
    selector: "app-profile",
    templateUrl: "./profile.component.html",
    styleUrls: ["./profile.component.css"],
})
export class ProfileComponent implements OnInit {

    constructor(private userService: UserService,
                private formBuilder: FormBuilder) {
    }

    public user$: Observable<User>;
    public changePasswordVisible = false;
    public changePasswordForm: FormGroup;
    public hideCurrentPassword = true;
    public hideNewPassword = true;
    public hideConfirmNewPassword = true;

    public ngOnInit(): void {
        this.user$ = this.userService.getById(localStorage.getItem("USER_ID"));
        this.changePasswordForm = this.createChangePasswordForm();
    }

    public createChangePasswordForm(): FormGroup {
        return this.formBuilder.group(
            {
                currentPassword: ["", Validators.required],
                newPassword: ["", PasswordValidate],
                confirmNewPassword: ["", Validators.required],
            },
            { validators: passwordsMatch("newPassword", "confirmNewPassword") },
        );
    }

    public changePasswordVisibility(): void {
        this.changePasswordVisible = !this.changePasswordVisible;
    }

    public changePasswordSubmit(): void {
        console.log(this.changePasswordForm.value.currentPassword);
        console.log(this.changePasswordForm.value.newPassword);
    }
}
