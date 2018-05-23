import { Component, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { BeerService } from '../../services/beer.service'
import { Observable } from 'rxjs';
import 'rxjs/Rx';
import { BeerDetail } from "../../shared/beer-detail.model";
import { ActivatedRoute } from '@angular/router';
import { NgProgress } from 'ngx-progressbar';

@Component({
	selector: 'beer-details',
	templateUrl: './beer-details.html',
	providers: [BeerService]
})
export class BeerDetailsComponent {
	public selectedStyle: any;
	public beerDetail: any = {};

	constructor(private _beerService: BeerService, private route: ActivatedRoute, public ngProgress: NgProgress) {

	}

	ngOnInit() {
		this.route.params.subscribe(params => {
				this.getBeerDetails(params.id);
			});

	}

	getBeerDetails(beerId: string) {
		this.ngProgress.start();
		this._beerService.getBeerById(beerId).subscribe(
			data => {
				this.beerDetail = data;
				this.ngProgress.done();
			}
		);
	}
}