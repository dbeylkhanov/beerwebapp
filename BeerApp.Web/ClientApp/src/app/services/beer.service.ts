import { catchError } from 'rxjs/operators';
import { Injectable, Inject } from '@angular/core';
import { Beer, BeerStyle } from '../shared/models/beer.model';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { throwError } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class BeerService {
  private myAppUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getBeers(query?: any) {
    let params = {};
    if (query) {
      params = new HttpParams({
        fromString: `query=${query}`
      });
    }
    return this.http.get<Beer[]>(this.myAppUrl + 'api/Beers/', { params: params })
      .pipe(catchError(this.errorHandler));

  }

  getBeersByStyle(styleId: any) {
    return this.http.get<Beer[]>(this.myAppUrl + 'api/Beers/?styleId=' + styleId)
      .pipe(catchError(this.errorHandler));
  }

  getBeerStyles() {
    return this.http.get<BeerStyle[]>(this.myAppUrl + 'api/BeerStyles/')
      .pipe(catchError(this.errorHandler));
  }

  getBeerById(id: string) {
    return this.http.get<Beer>(this.myAppUrl + 'api/Beer/' + id)
      .pipe(catchError(this.errorHandler));
  }

  private errorHandler(err: HttpErrorResponse) {
    let errorMessage;
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
