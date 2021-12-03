import { Injectable, Injector } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Jwt, LoginRequest, RegisterRequest } from "../models/jwt";
import { map, Observable } from "rxjs";
import { CookieService } from "ngx-cookie-service";
import { WidgetsService } from "./widgets.service";
import { Widget } from "../models/widget";

@Injectable({
	providedIn: "root"
})
export class AuthService
{
	private _jwt: Jwt | null = null;

	public get accessToken(): string | null
	{
		return this._jwt?.access_token || null;
	}

	constructor(
		private _http: HttpClient,
		private _cookies: CookieService,
		private _injector: Injector
	)
	{
		if (this._cookies.check("jwt"))
			this._jwt = JSON.parse(this._cookies.get("jwt"));
	}

	login(request: LoginRequest): Observable<void>
	{
		return this._http.post<Jwt>(`${environment.apiUrl}/auth/login`, request)
			.pipe(map(x =>
			{
				this._jwt = x;
				this._cookies.set("jwt", JSON.stringify(this._jwt));
				// noinspection JSIgnoredPromiseFromCall
				this._injector.get(WidgetsService).refreshWidgets();
			}));
	}

	register(request: RegisterRequest): Observable<void>
	{
		return this._http.post<Jwt>(`${environment.apiUrl}/auth/register`, request)
			.pipe(map(x =>
			{
				this._jwt = x;
				this._cookies.set("jwt", JSON.stringify(this._jwt));
			}));
	}

	logout(): void
	{
		this._jwt = null;
		this._cookies.delete("jwt");
	}
}
