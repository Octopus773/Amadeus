import { Component } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { AuthService } from "../../services/auth.service";
import { LoginRequest, RegisterRequest } from "../../models/jwt";

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
		private _auth: AuthService
	)
	{}

	signUp(): void
	{
		this._auth.register(this.signUpForm);
	}

	signIn(): void
	{
		this._auth.login(this.signInForm);
	}
}
