import { Directive, Input, Type, ViewContainerRef } from "@angular/core";
import { WeatherComponent } from "../components/weather/weather.component";
import { Widget } from "../models/widget";
import { ForecastComponent } from "../components/forecast/forecast.component";
import { NotFoundComponent } from "../components/not-found/not-found.component";

@Directive({
	selector: "[hostWidgetType]"
})
export class DashboardWidgetDirective
{
	@Input() set hostWidgetType(widget: Widget)
	{
		this.viewContainerRef.clear();
		this.viewContainerRef.createComponent(this._widgetFactory(widget.type));
	}

	constructor(private viewContainerRef: ViewContainerRef)
	{}

	private _widgetFactory(type: string): Type<unknown>
	{
		switch (type)
		{
		case "weather":
			return WeatherComponent;
		case "forecast":
			return ForecastComponent;
		default:
			return NotFoundComponent;
		}
	}
}
