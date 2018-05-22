import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';

import { Beers } from "./components/beers/beers.component";

import {OrderByPipe} from "./pipes/orderby-pipe"

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		OrderByPipe,
		Beers
	],
	imports: [
		CommonModule,
		HttpModule,
		FormsModule,
		RouterModule.forRoot([
			{ path: '', redirectTo: 'home', pathMatch: 'full' },
			{ path: 'home', component: HomeComponent },
			{ path: '**', redirectTo: 'home' }
		])
	],

	bootstrap: [AppComponent]
})
export class AppModuleShared {
}
