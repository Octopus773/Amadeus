import { ComponentRef, Directive, Input, Type, ViewContainerRef } from "@angular/core";
import { WeatherComponent } from "../components/weather/weather.component";
import { Widget } from "../models/widget";
import { ForecastComponent } from "../components/forecast/forecast.component";
import { WidgetComponent } from "./widget.component";
import { CovidComponent } from "../components/covid/covid.component";

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
		console.log(widget);
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
		default:
			throw new Error("Invalid component");
		}
	}
}
