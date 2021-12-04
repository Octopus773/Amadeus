import { Component } from "@angular/core";
import { WidgetComponent } from "../../misc/widget.component";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { WidgetsService } from "../../services/widgets.service";
import { Anime } from "../../models/anime";

@Component({
	selector: "host-watchlist",
	templateUrl: "./watchlist.component.html",
	styleUrls: ["./watchlist.component.scss"]
})
export class WatchlistComponent extends WidgetComponent
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
		let url = `${environment.apiUrl}/anilist/watchlist`;
		if (this.widget.parameters["user"])
			url += `/${this.widget.parameters["user"]}`;
		// eslint-disable-next-line @typescript-eslint/no-explicit-any
		await this._http.get<Anime[]>(url)
			.subscribe(x => this.info = x);
	}

	async updateWidget(): Promise<void>
	{
		await this._widgets.update(this.widget);
		await this.refresh();
	}
}
