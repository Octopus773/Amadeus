import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { Module } from "../models/module";

@Injectable({
	providedIn: "root"
})
export class LookupService 
{
	loadModules(): Observable<Module[]>
	{
		const modules: Module[] = [
			{
				displayName: "weather",
				remoteEntry: "http://localhost:4201/remoteEntry.js",
				remoteName: "weather",
				routePath: "weather",
				exposedModule: "./Module",
				ngModuleName: "WeatherModule",
			}
		];
		return of(modules);
	}
}
