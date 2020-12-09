import {
    Component,
    ElementRef,
    OnInit,
    ViewChild,
} from "@angular/core";
import {
    FormBuilder,
    FormGroup,
    Validators,
} from "@angular/forms";
import { UserService } from "../authorization-module/services/user.service";
import { passwordsMatch } from "../authorization-module/services/validators/password-match.validator";
import { passwordValidate } from "../authorization-module/services/validators/password.validator";
import { IUpdateUserDto } from "../models/update-user-dto";
import { IUser } from "../models/user";

@Component({
    selector: "app-profile",
    templateUrl: "./profile.component.html",
    styleUrls: ["./profile.component.css"],
})
export class ProfileComponent implements OnInit {

    constructor(private userService: UserService,
                private formBuilder: FormBuilder) {
    }

    public user: IUser;
    public changePasswordForm: FormGroup;
    public profileForm: FormGroup;
    public changePasswordVisible = false;
    public hideCurrentPassword = true;
    public hideNewPassword = true;
    public hideConfirmNewPassword = true;
    @ViewChild("profileImage") private profileImageRef: ElementRef;
    private MAX_FILE_SIZE = 6291456;

    public ngOnInit(): void {
        this.userService.getById(localStorage.getItem("USER_ID"))
            .subscribe(
                (user: IUser) => {
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
        const updateUserDto: IUpdateUserDto = {
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
        const updateUserDto: IUpdateUserDto = {
            userId: this.user.id,
            username: this.profileForm.value.username,
            email: this.profileForm.value.email,
        };
        this.userService.updateUser(updateUserDto)
            .subscribe(
                () => {},
                error => {
                    const errorMessage = error.error.split(":")[1];
                    alert(errorMessage);
                },
            );
    }

    public fileSelected(event: Event): void {
        // @ts-ignore
        const file: File = event.target.files[0];
        this.profileImageRef.nativeElement.value = "";
        if (file.size > this.MAX_FILE_SIZE) {
            alert("Image so big! Max size is 6Mb.");
            return;
        }
        this.userService.uploadPhoto(this.user.id, file)
            .subscribe(
                (res) => {
                    this.user.photoLink = res.body.value;
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
                newPassword: ["", passwordValidate],
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
