import { BeerStyle } from "./beer-style.model";
import { Brewery } from "./brewery.model";

export interface BeerDetail {
	id: string;
	name: string;
	description: string;
	abv: number;
	style: BeerStyle;
	breweries: Brewery[];
	ibu:number;
	statusDisplay:string;
}
