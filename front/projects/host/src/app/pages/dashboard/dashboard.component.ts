import { Component } from "@angular/core";
import { WidgetsService } from "../../services/widgets.service";
import { AuthService } from "../../services/auth.service";

@Component({
	selector: "host-dashboard",
	templateUrl: "./dashboard.component.html",
	styleUrls: ["./dashboard.component.scss"]
})
export class DashboardComponent
{
	constructor(
		public widgets: WidgetsService,
		public auth: AuthService
	)
	{}
}
