export class Widget
{
	id: number;
	type: string;
	parameters: {[key: string]: unknown};

	constructor(type: string)
	{
		this.type = type;
		this.parameters = {};
	}
}
