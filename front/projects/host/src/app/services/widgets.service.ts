import { Injectable } from "@angular/core";
import { Widget } from "../models/widget";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { moveItemInArray } from "@angular/cdk/drag-drop";

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

	createWidget(widgetType: string): Promise<void>
	{
		const widget: Widget = new Widget(widgetType);
		return this.addWidget(widget);
	}

	async addWidget(widget: Widget): Promise<void>
	{
		await this._http.post<Widget>(`${environment.apiUrl}/widget`, {type: widget.type, parameters: widget.parameters})
			.subscribe(x => this._widgets.push(x));
	}

	async update(widget: Widget): Promise<void>
	{
		await this._http.put(`${environment.apiUrl}/widget/${widget.id}`, widget).subscribe();
	}

	async delete(widget: Widget): Promise<void>
	{
		await this._http.delete(`${environment.apiUrl}/widget/${widget.id}`)
			.subscribe(() => this._widgets = this._widgets.filter(x => x != widget));
	}

	async refreshWidgets(): Promise<void>
	{
		await this._http.get<Widget[]>(`${environment.apiUrl}/widget`)
			.subscribe(x => this._widgets = x);
	}

	async reorder(from: number, to: number): Promise<void>
	{
		moveItemInArray(this._widgets, from, to);
		await this._http.post(`${environment.apiUrl}/widget/reorder?from=${from + 1}&to=${to + 1}`, {}).subscribe();
	}
}
