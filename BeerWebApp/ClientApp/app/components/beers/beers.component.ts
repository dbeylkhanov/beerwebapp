import { Component, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { BeerService } from '../../services/beer.service'
import { Observable } from 'rxjs/Rx';
import { BeerStyle } from "../../shared/beer-style.model";
import { Beer } from "../../shared/beer.model";
import { NgProgress } from 'ngx-progressbar';

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
	//
	private beerList: Beer[] = [];
	private beerStyles: BeerStyle[] = [];
	private defaultBeerStyle = <BeerStyle>{ id: 0, name: 'All' };

	constructor(private _beerService: BeerService, public ngProgress: NgProgress) {

	}

	@ViewChild('searchRef', {read: ElementRef}) searchRef: ElementRef;

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
		this.ngProgress.start();

		this._beerService.getBeers(query).then((result) => {

			this.beerList = this.filteredBeerList = result;
			if (this.query && this.query.length > 0) {
				this.filteredBeerStylesList = this.beerStyles.filter(o1 => o1.id === 0 || this.beerList.some(o2 => o2.style != null && o1.id === o2.style.id));
				this.selectedStyle = this.filteredBeerStylesList[0];
			} else {
				this.filteredBeerStylesList = this.beerStyles;
				this.selectedStyle = this.filteredBeerStylesList[0];
			}
			this.ngProgress.done();
		}, (error) => {
			this.beerList.length = this.filteredBeerList.length = 0;
			this.ngProgress.done();
			console.log(error)
		}
		).catch(error => {
			this.ngProgress.done();
			console.log(error)
		});
	}

	getBeersByStyle(style: any) {

		if (!this.query && this.query.length === 0) {
			this.ngProgress.start();
			this._beerService.getBeersByStyle(style.id).then(
				(data) => {
					this.beerList = this.filteredBeerList = data;
					this.ngProgress.done();
				}, (error) => {
					this.ngProgress.done();
					console.log(error)
				}
			).catch(error => {
				this.ngProgress.done();
				console.log(error)
			});

		} else {
			if (style.id !== this.defaultBeerStyle.id)
				this.filteredBeerList = this.beerList.filter(x => x.style.id === style.id);
			else
				this.filteredBeerList = this.beerList;
		}
	}

	get noResults(): boolean {
		return this.filteredBeerList.length === 0;
	}

	getBeerStyles() {
		this._beerService.getBeerStyles().then((result) => {
			this.beerStyles = result;

			this.beerStyles.splice(0, 0, this.defaultBeerStyle);

			this.filteredBeerStylesList = this.beerStyles;
			this.selectedStyle = this.filteredBeerStylesList[0];
		}, (error) => {
			console.log(error)
		}
		).catch(error => console.log(error));
	}
}


