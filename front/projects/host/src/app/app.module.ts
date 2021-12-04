import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { NotFoundComponent } from "./components/not-found/not-found.component";
import { DashboardComponent } from "./pages/dashboard/dashboard.component";

import { MatToolbarModule } from "@angular/material/toolbar";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatTooltipModule } from "@angular/material/tooltip";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { LoginComponent } from "./pages/login/login.component";
import { FormsModule } from "@angular/forms";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { MatMenuModule } from "@angular/material/menu";
import { AuthorizerInterceptor } from "./misc/authorization-interceptor.service";
import { WeatherComponent } from "./components/weather/weather.component";
import { DashboardWidgetDirective } from "./misc/dashboard-widget.directive";
import { ForecastComponent } from "./components/forecast/forecast.component";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";
import { MatExpansionModule } from "@angular/material/expansion";
import { CovidComponent } from "./components/covid/covid.component";
import { OauthCodeComponent } from "./pages/oauth-code/oauth-code.component";
import { CookieService } from "ngx-cookie-service";
import { WatchlistComponent } from './components/watchlist/watchlist.component';
import { MatListModule } from "@angular/material/list";

@NgModule({
	declarations: [
		AppComponent,
		NotFoundComponent,
		DashboardComponent,
		LoginComponent,
		WeatherComponent,
		DashboardWidgetDirective,
		ForecastComponent,
		CovidComponent,
		OauthCodeComponent,
  WatchlistComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		MatToolbarModule,
		MatIconModule,
		MatButtonModule,
		MatTooltipModule,
		MatCardModule,
		FormsModule,
		HttpClientModule,
		MatMenuModule,
		BrowserAnimationsModule,
		MatFormFieldModule,
		MatInputModule,
		MatSelectModule,
		MatExpansionModule,
		MatListModule
	],
	providers: [
		CookieService,
		{
			provide: HTTP_INTERCEPTORS,
			useClass: AuthorizerInterceptor,
			multi: true
		}
	],
	bootstrap: [AppComponent]
})
export class AppModule
{
}
