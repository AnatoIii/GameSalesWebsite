import { FormGroup } from "@angular/forms";

export function passwordsMatch(password: string, confirmPassword: string): (formGroup: FormGroup) => null {
    return (formGroup: FormGroup) => {
        const passwordControl = formGroup.controls[password];
        const confirmPasswordControl = formGroup.controls[confirmPassword];

        if (confirmPasswordControl.errors) {
            return null;
        }

        if (passwordControl.value !== confirmPasswordControl.value) {
            confirmPasswordControl.setErrors({ mustMatch: true });
        } else {
            confirmPasswordControl.setErrors(null);
        }
    };
}
