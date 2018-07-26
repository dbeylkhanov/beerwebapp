import { Component } from '@angular/core';
import { BeerService } from '../services/beer.service';
import 'rxjs/Rx';
import { ActivatedRoute } from '@angular/router';
import { NgProgress } from 'ngx-progressbar';

@Component({
  selector: 'beer-details',
  templateUrl: './beer-details.html'
})
export class BeerDetailsComponent {
  public selectedStyle: any;
  public beerDetail: any = {};
  constructor(private _beerService: BeerService, private route: ActivatedRoute, public ngProgress: NgProgress) {

  }

  ngOnInit() {
    this.route.params
      .filter(params => params.id)
      .subscribe(params => {
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
