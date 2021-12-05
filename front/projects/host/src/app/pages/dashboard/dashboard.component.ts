import { Component } from "@angular/core";
import { WidgetsService } from "../../services/widgets.service";
import { AuthService } from "../../services/auth.service";
import { CdkDragDrop } from "@angular/cdk/drag-drop";

@Component({
	selector: "host-dashboard",
	templateUrl: "./dashboard.component.html",
	styleUrls: ["./dashboard.component.scss"]
})
export class DashboardComponent
{
	constructor(
		public widgets: WidgetsService,
		public auth: AuthService
	)
	{}

	async reorder(event: CdkDragDrop<number>): Promise<void>
	{
		await this.widgets.reorder(event.previousContainer.data, event.container.data);
	}
}
