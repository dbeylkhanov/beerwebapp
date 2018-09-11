
import {filter} from 'rxjs/operators';
import { Component } from '@angular/core';
import { BeerService } from '../services/beer.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'beer-details',
  templateUrl: './beer-details.html'
})
export class BeerDetailsComponent {
  public selectedStyle: any;
  public beerDetail: any = {};
  public loading: boolean = false;

  constructor(private _beerService: BeerService, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.route.params.pipe(
      filter(params => params.id))
      .subscribe(params => {
      this.getBeerDetails(params.id);
    });

  }

  getBeerDetails(beerId: string) {
    this.loading = true;
    this._beerService.getBeerById(beerId).subscribe(
      data => {
        this.beerDetail = data;
      }, error => {
        console.log(error);
      }, () => {
        this.loading = false;
      }
    );
  }
}
