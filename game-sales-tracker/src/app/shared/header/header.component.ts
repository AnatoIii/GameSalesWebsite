import {
    Component,
    OnInit,
} from "@angular/core";
import {
    NavigationEnd,
    Router,
    RouterEvent,
} from "@angular/router";
import { Observable } from "rxjs";
import {
    filter,
    map,
} from "rxjs/operators";
import { AuthService } from "../../services/auth.service";

@Component({
    selector: "app-header",
    templateUrl: "./header.component.html",
    styleUrls: ["./header.component.css"],
})
export class HeaderComponent implements OnInit {

    constructor(public authService: AuthService,
                private router: Router) {
    }

    public currentUrl$: Observable<string>;

    public ngOnInit(): void {
        this.currentUrl$ = this.router.events
            .pipe(
                filter(event => event instanceof NavigationEnd),
                map((event: RouterEvent) => event.url),
            );
    }

    public logOut(): void {
        this.authService.logOut();
        this.router.navigate(["/login"]);
    }
}
