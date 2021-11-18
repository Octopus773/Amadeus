import { loadRemoteModule, LoadRemoteModuleOptions } from "@angular-architects/module-federation";
import { Route } from "@angular/router";

export type Module = LoadRemoteModuleOptions & {
	displayName: string;
	routePath: string;
	ngModuleName: string;
}

export function toRoute(module: Module): Route
{
	return {
		path: module.routePath,
		loadChildren: () => loadRemoteModule(module)
			.then(m => m[module.ngModuleName])
	};
}