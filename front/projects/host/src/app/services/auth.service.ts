import { Injectable, Injector } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Jwt, LoginRequest, RegisterRequest } from "../models/jwt";
import { map, Observable } from "rxjs";
import { CookieService } from "ngx-cookie-service";
import { WidgetsService } from "./widgets.service";
import { User } from "../models/user";

@Injectable({
	providedIn: "root"
})
export class AuthService
{
	private _jwt: Jwt | null = null;
	private _user: User | null = null;

	public get accessToken(): string | null
	{
		return this._jwt?.access_token || null;
	}

	public get user(): User | null
	{
		return this._user;
	}

	constructor(
		private _http: HttpClient,
		private _cookies: CookieService,
		private _injector: Injector
	)
	{
		if (this._cookies.check("jwt"))
		{
			this._jwt = JSON.parse(this._cookies.get("jwt"));
			setTimeout(() =>
			{
				this._http.get<User>(`${environment.apiUrl}/auth/me`).subscribe(x => this._user = x)
			}, 1);
		}
	}

	private _useToken(token: Jwt): void
	{
		this._jwt = token;
		this._cookies.set(
			"jwt",
			JSON.stringify(this._jwt),
			{
				expires: new Date(2037, 1),
				secure: true,
				sameSite: "Strict",
				path: "/"
			}
		);
		// noinspection JSIgnoredPromiseFromCall
		this._injector.get(WidgetsService).refreshWidgets();
		this._http.get<User>(`${environment.apiUrl}/auth/me`).subscribe(x => this._user = x);
	}

	login(request: LoginRequest): Observable<void>
	{
		return this._http.post<Jwt>(`${environment.apiUrl}/auth/login`, request)
			.pipe(map(x => this._useToken(x)));
	}

	loginVia(api: string,  code: string): Observable<void>
	{
		return this._http.post<Jwt>(`${environment.apiUrl}/auth/login/${api}?code=${code}`, {})
			.pipe(map(x => this._useToken(x)));
	}

	register(request: RegisterRequest): Observable<void>
	{
		return this._http.post<Jwt>(`${environment.apiUrl}/auth/register`, request)
			.pipe(map(x => this._useToken(x)));
	}

	logout(): void
	{
		this._jwt = null;
		this._cookies.delete("jwt");
	}

	anilistRedirect(): void
	{
		window.location.href = `${environment.apiUrl}/auth/anilist?redirectUrl=${window.location.origin}/login/anilist`;
	}
}
