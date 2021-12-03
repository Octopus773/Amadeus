import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NotFoundComponent } from "./components/not-found/not-found.component";
import { DashboardComponent } from "./pages/dashboard/dashboard.component";
import { LoginComponent } from "./pages/login/login.component";
import { OauthCodeComponent } from "./pages/oauth-code/oauth-code.component";

export const routes: Routes = [
	{path: "", pathMatch: "full", component: DashboardComponent},
	{path: "login", pathMatch: "full", component: LoginComponent},
	{path: "login/:api", pathMatch: "full", component: OauthCodeComponent},
	{path: "**", component: NotFoundComponent}
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule
{ }
