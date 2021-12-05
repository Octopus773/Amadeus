import { Component } from "@angular/core";
import { WidgetComponent } from "../../misc/widget.component";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { WidgetsService } from "../../services/widgets.service";
import { Anime } from "../../models/anime";

@Component({
	selector: "host-trends",
	templateUrl: "./trends.component.html",
	styleUrls: ["./trends.component.scss"]
})
export class TrendsComponent extends WidgetComponent
{
	public info: Anime[] = [];

	constructor(
		private _http: HttpClient,
		private _widgets: WidgetsService
	)
	{
		super();
	}

	async refresh(): Promise<void>
	{
		if (!this.widget.parameters["type"])
			return;
		// eslint-disable-next-line @typescript-eslint/no-explicit-any
		await this._http.get<Anime[]>(`${environment.apiUrl}/anilist/list/${this.widget.parameters["type"]}`)
			.subscribe(x => this.info = x);
	}

	async updateWidget(): Promise<void>
	{
		await this._widgets.update(this.widget);
		await this.refresh();
	}
}
