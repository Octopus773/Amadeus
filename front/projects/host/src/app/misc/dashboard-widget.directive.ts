import { ComponentRef, Directive, Input, Type, ViewContainerRef } from "@angular/core";
import { WeatherComponent } from "../components/weather/weather.component";
import { Widget } from "../models/widget";
import { ForecastComponent } from "../components/forecast/forecast.component";
import { WidgetComponent } from "./widget.component";
import { CovidComponent } from "../components/covid/covid.component";
import { WatchlistComponent } from "../components/watchlist/watchlist.component";
import { AnimeComponent } from "../components/anime/anime.component";
import { TrendsComponent } from "../components/trends/trends.component";

@Directive({
	selector: "[hostWidgetType]"
})
export class DashboardWidgetDirective
{
	@Input() set hostWidgetType(widget: Widget)
	{
		this.viewContainerRef.clear();
		const ref: ComponentRef<WidgetComponent> = this.viewContainerRef.createComponent<WidgetComponent>(
			DashboardWidgetDirective._widgetFactory(widget.type)
		);
		widget.parameters ??= {};
		ref.instance.widget = widget;
		ref.instance._setupRefresh();
	}

	constructor(private viewContainerRef: ViewContainerRef)
	{}

	private static _widgetFactory(type: string): Type<WidgetComponent>
	{
		switch (type)
		{
		case "weather":
			return WeatherComponent;
		case "forecast":
			return ForecastComponent;
		case "covid":
			return CovidComponent;
		case "watchlist":
			return WatchlistComponent;
		case "anime":
			return AnimeComponent;
		case "trends":
			return TrendsComponent;
		default:
			throw new Error("Invalid component");
		}
	}
}
