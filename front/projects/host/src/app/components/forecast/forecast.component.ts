import { Component } from "@angular/core";
import { WidgetComponent } from "../../misc/widget.component";

@Component({
	selector: "host-forecast",
	templateUrl: "./forecast.component.html",
	styleUrls: ["./forecast.component.scss"]
})
export class ForecastComponent extends WidgetComponent
{
	refresh(): Promise<void>
	{
		return Promise.resolve(undefined);
	}
}
