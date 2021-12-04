import { Widget } from "../models/widget";
import { Component, OnDestroy } from "@angular/core";

@Component({template: ""})
export abstract class WidgetComponent implements OnDestroy
{
	protected _refresher: number;
	widget: Widget;

	abstract refresh(): Promise<void>;

	_setupRefresh(): void
	{
		// noinspection JSIgnoredPromiseFromCall
		this.refresh();
		this._refresher = setInterval(async () => {
			await this.refresh();
		}, 5000);
	}

	ngOnDestroy(): void
	{
		clearInterval(this._refresher);
	}
}
