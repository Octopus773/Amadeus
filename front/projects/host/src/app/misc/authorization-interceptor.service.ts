import { Injectable, Injector } from "@angular/core";
import {
	HttpRequest,
	HttpHandler,
	HttpEvent,
	HttpInterceptor
} from "@angular/common/http";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth.service";

@Injectable()
export class AuthorizerInterceptor implements HttpInterceptor
{
	constructor(private auth: AuthService)
	{}

	intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>>
	{
		const token: string | null = this.auth.accessToken;
		if (token)
			request = request.clone({setHeaders: {Authorization: "Bearer " + token}});
		return next.handle(request);
	}
}
