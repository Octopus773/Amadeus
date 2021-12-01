import { Injectable } from "@angular/core";
import { WeatherWidget, Widget } from "../models/widget";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";

@Injectable({
	providedIn: "root"
})
export class WidgetsService
{
	private _widgets: Widget[] = [];
	public get widgets(): Widget[]
	{
		return this._widgets;
	}

	constructor(
		private _http: HttpClient
	)
	{
		// noinspection JSIgnoredPromiseFromCall
		this.refreshWidgets();
	}

	_widgetFactory(type: string): Widget
	{
		switch (type)
		{
		case "weather":
			return new WeatherWidget();
		default:
			throw new Error("Invalid widget type: " + type);
		}
	}

	_widgetType(widget: Widget): string
	{
		if (widget instanceof WeatherWidget)
			return "weather";
		throw new Error("Invalid widget type.");
	}

	createWidget(widgetType: string): Promise<void>
	{
		const widget: Widget = this._widgetFactory(widgetType);
		return this.addWidget(widget);
	}

	async addWidget(widget: Widget): Promise<void>
	{
		await this._http.post<Widget>(`${environment.apiUrl}/widget`, {type: this._widgetType(widget), parameters: widget})
			.subscribe(x => this._widgets.push(x));
	}

	async delete(widget: Widget): Promise<void>
	{
		await this._http.delete(`${environment.apiUrl}/widget`, widget)
			.subscribe(() => this._widgets = this._widgets.filter(x => x != widget));
	}

	async refreshWidgets(): Promise<void>
	{
		await this._http.get<Widget[]>(`${environment.apiUrl}/widget`)
			.subscribe(x => this._widgets = x);
	}
}
