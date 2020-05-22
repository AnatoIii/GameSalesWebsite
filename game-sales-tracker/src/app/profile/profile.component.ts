import {
    Component,
    OnInit,
} from "@angular/core";
import {
    FormBuilder,
    FormGroup,
    Validators,
} from "@angular/forms";
import { UserService } from "../services/user.service";
import { UpdateUserDto } from "../shared/models/update-user-dto";
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

    public user: User;
    public changePasswordForm: FormGroup;
    public profileForm: FormGroup;
    public changePasswordVisible = false;
    public hideCurrentPassword = true;
    public hideNewPassword = true;
    public hideConfirmNewPassword = true;

    public ngOnInit(): void {
        this.userService.getById(localStorage.getItem("USER_ID"))
            .subscribe(
                (user: User) => {
                    this.user = user;
                    this.profileForm.setValue({ username: this.user.username, email: this.user.email });
                },
            );
        this.createChangePasswordForm();
        this.createProfileForm();
    }

    public changePasswordVisibility(): void {
        this.changePasswordVisible = !this.changePasswordVisible;
    }

    public changePasswordSubmit(): void {
        const updateUserDto: UpdateUserDto = {
            userId: localStorage.getItem("USER_ID"),
            currentPassword: this.changePasswordForm.value.currentPassword,
            password: this.changePasswordForm.value.newPassword,
        };
        this.userService.updateUser(updateUserDto)
            .subscribe(
                () => {
                    this.changePasswordForm.reset();
                    this.changePasswordVisibility();
                },
                error => {
                    const errorMessage = error.error.split(":")[1];
                    alert(errorMessage);
                },
            );
    }

    public changeProfileSubmit(): void {
        if (this.profileForm.invalid) {
            return;
        }
        const updateUserDto: UpdateUserDto = {
            userId: this.user.id,
            username: this.profileForm.value.username,
            email: this.profileForm.value.email,
        };
        this.userService.updateUser(updateUserDto)
            .subscribe(
                () => {
                    console.log("successfully updated");
                },
                error => {
                    const errorMessage = error.error.split(":")[1];
                    alert(errorMessage);
                },
            );
    }

    private createChangePasswordForm(): void {
        this.changePasswordForm = this.formBuilder.group(
            {
                currentPassword: ["", Validators.required],
                newPassword: ["", PasswordValidate],
                confirmNewPassword: ["", Validators.required],
            },
            { validators: passwordsMatch("newPassword", "confirmNewPassword") },
        );
    }

    private createProfileForm(): void {
        this.profileForm = this.formBuilder.group(
            {
                username: ["", [
                    Validators.required,
                    Validators.minLength(3),
                    Validators.maxLength(20)]],
                email: ["", [
                    Validators.required,
                    Validators.email]],
            },
        );
    }
}
