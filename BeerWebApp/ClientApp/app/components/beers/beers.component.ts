import { Component, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { BeerService } from '../../services/beer.service'
import { Observable } from 'rxjs';
import 'rxjs/Rx';
import { BeerStyle } from "../../shared/beer-style.model";
import { Beer } from "../../shared/beer.model";


@Component({
	selector: 'beers',
	templateUrl: './beers.html',
	providers: [BeerService]
})

export class BeersComponent implements AfterViewInit {
	selectedSort = 'abv';
	query = '';
	public selectedStyle: any;
	public filteredBeerList: Beer[] = [];
	public filteredBeerStylesList: BeerStyle[] = [];
	public noResults: boolean = false;
	//
	private beerList: Beer[] = [];
	private beerStyles: BeerStyle[] = [];
	private defaultBeerStyle = <BeerStyle>{ id: 0, name: 'All' };

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
				this.beerList = this.filteredBeerList = data;
				if (this.query && this.query.length > 0) {
					this.filteredBeerStylesList = this.beerStyles.filter(o1 => o1.id === 0 || this.beerList.some(o2 => o2.style != null && o1.id === o2.style.id));
					this.selectedStyle = this.filteredBeerStylesList[0];
				} else {
					this.filteredBeerStylesList = this.beerStyles;
					this.selectedStyle = this.filteredBeerStylesList[0];
				}
				this.checkBeerResultsCount();
			}
		);
	}

	getBeersByStyle(style: any) {
		if (!this.query && this.query.length === 0) {
			this._beerService.getBeersByStyle(style.id).subscribe(
				data => {
					this.beerList = this.filteredBeerList = data;
				}
			);
			this.checkBeerResultsCount();
		} else {
			if (style.id !== this.defaultBeerStyle.id)
				this.filteredBeerList = this.beerList.filter(x => x.style.id === style.id);
			else
				this.filteredBeerList = this.beerList;
			this.checkBeerResultsCount();
		}

	}

	checkBeerResultsCount() {
		this.noResults = this.filteredBeerList.length == 0;
	}

	getBeerStyles() {
		this._beerService.getBeerStyles().subscribe(
			data => {
				this.beerStyles = data;

				this.beerStyles.splice(0, 0, this.defaultBeerStyle);

				this.filteredBeerStylesList = this.beerStyles;
				this.selectedStyle = this.filteredBeerStylesList[0];
			}
		);
	}
}


