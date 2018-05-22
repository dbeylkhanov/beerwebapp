import { Component, ElementRef, ViewChild, AfterViewInit  } from '@angular/core';
import { OrderByPipe } from "../../pipes/orderby-pipe";
import { BeerService } from '../../services/beer.service'
import { Observable } from 'rxjs';
import { Http } from '@angular/http'
import 'rxjs/Rx';

@Component({
	selector: 'beers',
	templateUrl: './beers.html',
	providers: [BeerService]
})

export class Beers  implements AfterViewInit{
	selectedSort = 'abv';
	public selectedStyle: any;
	public beerList: Beer[];
	public beerStyles: BeerStyle[];
	query = '';

	constructor(private _beerService: BeerService) {
	
	}

	@ViewChild('searchRef') searchRef: ElementRef;

	ngOnInit() {
		this.getBeers();

		this.getBeerStyles();
	}

	ngAfterViewInit() {
		Observable.fromEvent(this.searchRef.nativeElement, 'keyup')
			.map((evt: any) => evt.target.value)
			.debounceTime(1000)
			.distinctUntilChanged()
			.subscribe((text: string) => {
				this.selectedStyle = this.beerStyles[0];
				this.getBeers(text);
			});
	}

	getBeers(query?: string) {
		this._beerService.getBeers(query).subscribe(
			data => {
				this.beerList = data;
			}
		);
	}

	getBeersByStyle(style:any) {
		this._beerService.getBeersByStyle(style.id).subscribe(
			data => {
				this.beerList = data;
			}
		);
	}

	getBeerStyles() {
		this._beerService.getBeerStyles().subscribe(
			data => {
				this.beerStyles = data;
				
				var allStyles: BeerStyle = { id: 0, name: 'All' };
				this.beerStyles.splice(0, 0, allStyles);
				this.selectedStyle = this.beerStyles[0];
			}
		);
	}
}

interface Beer {
	id: string;
	name: string;
	description: string;
	abv: number;
	styleId: number;
}

interface BeerStyle {
	id: number;
	name: string;
}
