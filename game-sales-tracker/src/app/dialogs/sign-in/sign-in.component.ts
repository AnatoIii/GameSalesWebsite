import {
    Component,
    Inject,
    OnInit,
} from "@angular/core";
import {
    MAT_DIALOG_DATA,
    MatDialogRef,
} from "@angular/material/dialog";

export interface SignInData {
    username: string;
    password: string;
}

@Component({
    selector: "app-sign-in",
    templateUrl: "./sign-in.component.html",
    styleUrls: ["./sign-in.component.css"],
})
export class SignInComponent implements OnInit {

    constructor(
        public dialogRef: MatDialogRef<SignInComponent>,
        @Inject(MAT_DIALOG_DATA) public data: SignInData,
    ) {
    }

    public ngOnInit(): void {
    }

}
