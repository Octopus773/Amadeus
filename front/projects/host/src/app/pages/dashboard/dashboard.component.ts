import { Component } from "@angular/core";
import { WidgetsService } from "../../services/widgets.service";

@Component({
	selector: "host-dashboard",
	templateUrl: "./dashboard.component.html",
	styleUrls: ["./dashboard.component.scss"]
})
export class DashboardComponent
{
	constructor(
		public widgets: WidgetsService
	)
	{}
}
