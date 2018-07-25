import { Observable, throwError } from "rxjs";
import { catchError, map, tap } from 'rxjs/operators';
import { Injectable, Inject } from '@angular/core';
import { Response } from '@angular/http';
import { Beer, BeerStyle } from '../shared/models/beer.model';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";

@Injectable()
export class BeerService {
  myAppUrl: string = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getBeers(query?: any): Observable<Beer[]> {
    return this.http.get<Beer[]>(this.myAppUrl + 'api/Beers/' + (query != undefined ? '?query=' + query : ''))
      .pipe(catchError(this.errorHandler));

  }

  getBeersByStyle(styleId: any): Observable<Beer[]> {
    return this.http.get<Beer[]>(this.myAppUrl + 'api/Beers/?styleId=' + styleId)
      .pipe(catchError(this.errorHandler));
  }

  getBeerStyles(): Observable<BeerStyle[]> {
    return this.http.get<BeerStyle[]>(this.myAppUrl + "api/BeerStyles/")
      .pipe(catchError(this.errorHandler));
  }

  getBeerById(id: string): Observable<Beer> {
    return this.http.get<Beer>(this.myAppUrl + "api/Beer/" + id)
      .pipe(catchError(this.errorHandler));
  }

  private extractData(res: Response) {
    if (res.status === 200) {
      let body = res.json();
      return body || [];
    }
    console.log(res.status);
    return throwError(res);
  }

  private errorHandler(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };
}
