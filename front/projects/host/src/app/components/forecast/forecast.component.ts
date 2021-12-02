import { Component } from "@angular/core";
import { WidgetComponent } from "../../misc/widget.component";
import { HttpClient } from "@angular/common/http";
import { WidgetsService } from "../../services/widgets.service";
import { environment } from "../../../environments/environment";

@Component({
	selector: "host-forecast",
	templateUrl: "./forecast.component.html",
	styleUrls: ["./forecast.component.scss"]
})
export class ForecastComponent extends WidgetComponent
{
	public info: {weather: string, celsius: number, icon: string}[] = [];

	constructor(
		private _http: HttpClient,
		private _widgets: WidgetsService
	)
	{
		super();
	}

	async refresh(): Promise<void>
	{
		if (!this.widget.parameters["city"] || !this.widget.parameters["days"])
			return;
		// eslint-disable-next-line @typescript-eslint/no-explicit-any
		await this._http.get<any>(`${environment.apiUrl}/weather/${this.widget.parameters["city"]}/${this.widget.parameters["days"]}`)
			.subscribe(x =>
			{
				if (!this.info || this.info.length !== x.length)
					this.info = x;
				else
					this.info.map((_, i) => x[i]);
			});
	}

	async updateWidget(): Promise<void>
	{
		await this._widgets.update(this.widget);
		await this.refresh();
	}
}
