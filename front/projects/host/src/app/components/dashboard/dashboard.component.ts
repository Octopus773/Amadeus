import { Component } from "@angular/core";
import { Widget } from "../../models/widget";

@Component({
	selector: "host-dashboard",
	templateUrl: "./dashboard.component.html",
	styleUrls: ["./dashboard.component.scss"]
})
export class DashboardComponent
{
	public widgets: Widget[] = [{}];
}
