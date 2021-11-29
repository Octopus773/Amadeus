import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { WeatherRoutingModule } from "./weather-routing.module";
import { WeatherComponent } from "./weather.component";

@NgModule({
	declarations: [
		WeatherComponent
	],
	imports: [
		BrowserModule,
		WeatherRoutingModule
	],
	providers: [],
	bootstrap: [WeatherComponent]
})
export class WeatherModule 
{ }
