import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { NotFoundComponent } from "./components/not-found/not-found.component";
import { DashboardComponent } from "./components/dashboard/dashboard.component";

import { MatToolbarModule } from "@angular/material/toolbar";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { MatTooltipModule } from "@angular/material/tooltip";

@NgModule({
	declarations: [
		AppComponent,
		NotFoundComponent,
		DashboardComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		MatToolbarModule,
		MatIconModule,
		MatButtonModule,
		MatTooltipModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule
{ }
