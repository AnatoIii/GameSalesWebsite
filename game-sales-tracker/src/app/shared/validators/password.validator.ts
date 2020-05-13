import { AbstractControl } from "@angular/forms";

export function PasswordValidate(control: AbstractControl): { [key: string]: boolean } {
    const password: string = control.value;
    if (password === null || password === "") {
        return { required: true };
    }
    if (password.length < 8) {
        return { minLength: true };
    }
    if (password.length > 15) {
        return { maxlength: true };
    }
    if (!(new RegExp(/[A-Z]/).test(password))) {
        return { capitalCase: true };
    }
    if (!(new RegExp(/[a-z]/).test(password))) {
        return { lowerCase: true };
    }
    if (!(new RegExp(/[0-9]/).test(password))) {
        return { numbers: true };
    }
}
