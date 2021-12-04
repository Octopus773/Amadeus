import { Component } from "@angular/core";
import { WidgetComponent } from "../../misc/widget.component";
import { Anime } from "../../models/anime";
import { HttpClient } from "@angular/common/http";
import { WidgetsService } from "../../services/widgets.service";
import { environment } from "../../../environments/environment";

@Component({
	selector: "host-anime",
	templateUrl: "./anime.component.html",
	styleUrls: ["./anime.component.scss"]
})
export class AnimeComponent extends WidgetComponent
{
	public info: Anime | null = null;

	constructor(
		private _http: HttpClient,
		private _widgets: WidgetsService
	)
	{
		super();
	}

	async refresh(): Promise<void>
	{
		if (!this.widget.parameters["title"])
			return ;
		// eslint-disable-next-line @typescript-eslint/no-explicit-any
		await this._http.get<Anime>(`${environment.apiUrl}/anilist/anime/${this.widget.parameters["query"]}`)
			.subscribe(x => this.info = x);
	}

	async updateWidget(): Promise<void>
	{
		await this._widgets.update(this.widget);
		await this.refresh();
	}
}
