
import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';

@Injectable()
export class BeerService {
	myAppUrl: string = "";

	constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
		this.myAppUrl = baseUrl;
	}

	getBeers(query?: any): Promise<any> {
		return this.http.get(this.myAppUrl + 'api/Beers/' + (query != undefined ? '?query=' + query : ''))
			.toPromise()
			.then(this.extractData);
	}

	getBeersByStyle(styleId: any): Promise<any> {
		return this.http.get(this.myAppUrl + 'api/Beers/?styleId=' + styleId)
			.toPromise()
			.then(this.extractData);
	}

	getBeerStyles(): Promise<any> {
		return this.http.get(this.myAppUrl + "api/BeerStyles/")
			.toPromise()
			.then(this.extractData);
	}

	getBeerById(id: string): Promise<any> {
		return this.http.get(this.myAppUrl + "api/Beer/" + id)
			.toPromise()
			.then(this.extractData);
	}

	private extractData(res: Response): Promise<any> {
		if (res.status === 200) {
			let body = res.json();
			return Promise.resolve(body || []);
		}
		console.log(res.status);
		return Promise.reject(res);
	}

	errorHandler(error: Response): Promise<any> {
		console.error('An error occurred', error);
		return Promise.reject(error);
	}
}