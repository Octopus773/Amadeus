import { Component } from "@angular/core";
import { AuthService } from "../../services/auth.service";
import { LoginRequest, RegisterRequest } from "../../models/jwt";
import { Router } from "@angular/router";

@Component({
	selector: "host-login",
	templateUrl: "./login.component.html",
	styleUrls: ["./login.component.scss"]
})
export class LoginComponent
{
	public showSignUp = false;
	public signUpForm: RegisterRequest = new RegisterRequest();
	public signInForm: LoginRequest = new LoginRequest();

	constructor(
		public auth: AuthService,
		private _router: Router
	)
	{}

	signUp(): void
	{
		this.auth.register(this.signUpForm)
			.subscribe(() => this._router.navigateByUrl("/"));
	}

	signIn(): void
	{
		this.auth.login(this.signInForm)
			.subscribe(() => this._router.navigateByUrl("/"));
	}
}
