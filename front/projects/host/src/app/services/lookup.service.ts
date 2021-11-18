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
				remoteName: "weather",
				routePath: "weather",
				exposedModule: "weather",
				ngModuleName: "weather",
			}
		];
		return of(modules);
	}
}
