
import { Injectable, Inject } from '@angular/core';  
import { Http, Response } from '@angular/http';  
import { Observable } from 'rxjs/Observable';  
import { Router } from '@angular/router';  
import 'rxjs/add/operator/map';  
import 'rxjs/add/operator/catch';  
import 'rxjs/add/observable/throw';
import "rxjs/add/operator/toPromise";

@Injectable()
export class BeerService {
	myAppUrl: string = "";

	constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
		this.myAppUrl = baseUrl;
	}

	getBeers(query?:any): Promise<any[]> {
		return this.http.get(this.myAppUrl + 'api/Beers/' + (query != undefined ? '?query=' + query : ''))
			.toPromise()
			.then(this.extractData)
			.catch(this.handleError);
	}

	private extractData(res: Response) {
		let body = res.json();
		return body || [];
	}

	private handleError(error: any) {
		let errMsg = (error.message) ? error.message : error.status ? `${error.status} - ${error.statusText}` : 'Server error';
		console.error(errMsg);
		return Promise.reject(errMsg);
	}
}