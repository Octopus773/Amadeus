export class Widget
{
	id: number;
	type: string;
	parameters: {[key: string]: any};

	constructor(type: string)
	{
		this.type = type;
		this.parameters = {};
	}
}
