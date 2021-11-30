import { Component } from "@angular/core";
import { Router } from "@angular/router";

@Component({
	selector: "host-root",
	templateUrl: "./app.component.html",
	styleUrls: ["./app.component.scss"]
})
export class AppComponent
{
	constructor(
		// private _lookup: LookupService,
		// private  _router: Router
	)
	{
		// this._lookup.loadModules().subscribe(modules =>
		// {
		// 	this._router.resetConfig([
		// 		...modules.map(x => toRoute(x)),
		// 		...routes
		// 	]);
		// 	console.log(this._router.config);
		// 	console.log(modules);
		// });
	}
}
