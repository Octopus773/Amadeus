import { Component, ElementRef, ViewChild } from "@angular/core";

@Component({
	selector: "host-login",
	templateUrl: "./login.component.html",
	styleUrls: ["./login.component.scss"]
})
export class LoginComponent
{
	@ViewChild("container") private _container!: ElementRef<HTMLElement>;

	showSignUp()
	{
		this._container.nativeElement.classList.add("right-panel-active");
	}

	showSignIn()
	{
		this._container.nativeElement.classList.remove("right-panel-active");
	}
}
