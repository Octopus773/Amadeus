import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";

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
		private _http: HttpClient
	)
	{
		this._route.queryParams.subscribe(x =>
		{
			const api: string = this._route.snapshot.params["api"];
			this._http.post(`${environment.apiUrl}/auth/link/${api}?code=${x["code"]}`, {})
				.subscribe(() => this._router.navigate(["/"]));
		});
	}
}
