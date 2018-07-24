import { BeerStyle } from "./beer-style.model";

export interface Beer {
	id: string;
	name: string;
	description: string;
	abv: number;
	style: BeerStyle;
}