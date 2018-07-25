
export interface Beer {
	id: string;
	name: string;
	description: string;
	abv: number;
	style: BeerStyle;
}

export interface Brewery {
  name:string;
  website:string;
  description:string;
}

export interface BeerStyle {
  id: number;
  name: string;
  description: string;
}

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
