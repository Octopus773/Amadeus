import { Widget } from "../models/widget";

export abstract class WidgetComponent
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
}
