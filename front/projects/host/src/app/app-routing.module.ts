import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NotFoundComponent } from "./components/not-found/not-found.component";
import { DashboardComponent } from "./components/dashboard/dashboard.component";

export const routes: Routes = [
	{path: "", pathMatch: "full", component: DashboardComponent},
	{path: "**", component: NotFoundComponent}
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule
{ }
