import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Jwt, LoginRequest, RegisterRequest } from "../models/jwt";

@Injectable({
	providedIn: "root"
})
export class AuthService
{
	private _jwt: Jwt | null = null;

	public get accessToken(): string | null
	{
		return this._jwt?.accessToken || null;
	}

	constructor(
		private _http: HttpClient
	)
	{}

	login(request: LoginRequest): void
	{
		this._http.post<Jwt>(`${environment.apiUrl}/auth/login`, request)
			.subscribe(x => this._jwt = x);
	}

	register(request: RegisterRequest): void
	{
		this._http.post<Jwt>(`${environment.apiUrl}/auth/register`, request)
			.subscribe(x => this._jwt = x);
	}

	logout(): void
	{
		this._jwt = null;
	}
}
