import { Component } from "@angular/core";
import { WidgetsService } from "./services/widgets.service";
import { AuthService } from "./services/auth.service";

@Component({
	selector: "host-root",
	templateUrl: "./app.component.html",
	styleUrls: ["./app.component.scss"]
})
export class AppComponent
{
	constructor(
		private _widgets: WidgetsService,
		public auth: AuthService
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

	addWidget(type: string): void
	{
		this._widgets.createWidget(type);
	}
}
