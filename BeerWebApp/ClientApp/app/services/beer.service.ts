
import { Injectable, Inject } from '@angular/core';  
import { Http, Response } from '@angular/http';  
import { Observable } from 'rxjs/Observable';  
import { Router } from '@angular/router';  
import 'rxjs/add/operator/map';  
import 'rxjs/add/operator/catch';  
import 'rxjs/add/observable/throw';

@Injectable()
export class BeerService {
	myAppUrl: string = "";

	constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
		this.myAppUrl = baseUrl;
	}

	getBeers(query?:any){
		return this.http.get(this.myAppUrl + 'api/Beers/' + (query != undefined ? '?query=' + query : ''))
			.map(this.extractData)
			.catch(this.errorHandler);
	}

	getBeersByStyle(styleId:any){
		return this.http.get(this.myAppUrl + 'api/Beers/?styleId=' + styleId)
			.map(this.extractData)
			.catch(this.errorHandler);
	}
	
	getBeerStyles() {
		return this.http.get(this.myAppUrl + "api/BeerStyles/")
			.map(this.extractData)
			.catch(this.errorHandler);
	}

	getBeerById(id: string) {
		return this.http.get(this.myAppUrl + "api/Beer/" + id)
			.map(this.extractData)
			.catch(this.errorHandler);
	}

	private extractData(res: Response) {
		let body = res.json();
		return body || [];
	}

	errorHandler(error: Response) {
		console.log(error);
		return Observable.throw(error);
	}
}