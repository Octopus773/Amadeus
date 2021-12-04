import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { AuthService } from "../../services/auth.service";
import { Observable } from "rxjs";

@Component({
	selector: "host-oauth-code",
	templateUrl: "./oauth-code.component.html",
	styleUrls: ["./oauth-code.component.scss"]
})
export class OauthCodeComponent
{
	constructor(
		private _route: ActivatedRoute,
		private _router: Router,
		private _http: HttpClient,
		private _auth: AuthService
	)
	{
		this._route.queryParams.subscribe(x =>
		{
			const api: string = this._route.snapshot.params["api"];
			const code: string = x["code"];
			let sub: Observable<unknown>;
			if (this._auth.accessToken)
				sub = this._http.post(`${environment.apiUrl}/auth/link/${api}?code=${code}`, {});
			else
				sub = this._auth.loginVia(api, code);
			sub.subscribe(() =>
			{
				this._router.navigate(["/"]);
			});
		});
	}
}
