import {
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
    public intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        const interceptedReq = request.clone({ url: environment.apiUrl + request.url });
        return next.handle(interceptedReq);
    }
}
