import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { WidgetsService } from "../../services/widgets.service";
import { environment } from "../../../environments/environment";
import { WidgetComponent } from "../../misc/widget.component";

@Component({
	selector: "host-covid",
	templateUrl: "./covid.component.html",
	styleUrls: ["./covid.component.scss"]
})
export class CovidComponent extends WidgetComponent
{
	public info: {confirmed: number, recovered: number, critical: number, deaths: number} | null = null;

	constructor(
		private _http: HttpClient,
		private _widgets: WidgetsService
	)
	{
		super();
	}

	async refresh(): Promise<void>
	{
		let url = `${environment.apiUrl}/covid`;
		if (this.widget.parameters["country"])
			url += `/${this.widget.parameters["country"]}`;
		// eslint-disable-next-line @typescript-eslint/no-explicit-any
		await this._http.get<any>(url)
			.subscribe(x => this.info = x);
	}

	async updateWidget(): Promise<void>
	{
		await this._widgets.update(this.widget);
		await this.refresh();
	}
}
