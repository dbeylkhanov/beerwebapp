import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { BeerService } from '../../services/beer.service'

@Component({
	selector: 'settings',
	templateUrl: './settings.html',
	providers: [BeerService]
})
export class SettingsComponent {
	public apiKey = null;

	constructor(private _beerService: BeerService) {

	}

	saveApiKey(apiKey: string): void {
		if (apiKey != null)
			localStorage.setItem('beerwebapp_token', apiKey);
	}
}

