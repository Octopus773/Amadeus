import { Component } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { WidgetsService } from "../../services/widgets.service";
import { WidgetComponent } from "../../misc/widget.component";

@Component({
	selector: "host-weather",
	templateUrl: "./weather.component.html",
	styleUrls: ["./weather.component.scss"]
})
export class WeatherComponent extends WidgetComponent
{
	public info: {weather: string, celsius: number, icon: string} | null = null;

	constructor(
		private _http: HttpClient,
		private _widgets: WidgetsService
	)
	{
		super();
	}

	async refresh(): Promise<void>
	{
		if (!this.widget.parameters["city"])
			return;
		// eslint-disable-next-line @typescript-eslint/no-explicit-any
		await this._http.get<any>(`${environment.apiUrl}/weather/${this.widget.parameters["city"]}`)
			.subscribe(x => this.info = x);
	}

	async updateWidget(): Promise<void>
	{
		await this._widgets.update(this.widget);
		await this.refresh();
	}
}
