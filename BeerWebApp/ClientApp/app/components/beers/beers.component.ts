import {Component, ElementRef, ViewChild} from '@angular/core';
import {OrderByPipe} from "../../pipes/orderby-pipe";
import { BeerService } from '../../services/beer.service'  
import { Observable } from 'rxjs';
import { Http } from '@angular/http'
import 'rxjs/Rx';

@Component({
	selector: 'beers',
	templateUrl: './beers.html',
	providers: [BeerService]
})

export class Beers {
	selectedSort = 'abv';
	public beerList: Beer[];  
	query = '';

	constructor(private _beerService: BeerService) {  
	
	}

	@ViewChild('searchRef') searchRef: ElementRef;

	ngOnInit() {
		this.getBeers();

		Observable.fromEvent(this.searchRef.nativeElement, 'keyup')
			.map((evt: any) => evt.target.value)
			.debounceTime(1000)        
			.distinctUntilChanged()
			.subscribe((text: string) => this.getBeers(text));
	}

	getBeers(query?:string) {
		this._beerService.getBeers(query).then(
			data => {
				this.beerList = data;
			}
		);
	}  
}

interface Beer {  
	id:string;
	name: string;  
	description:string;
	abv:number;
}