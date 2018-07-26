import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { BeersComponent } from './beers/beers.component';

import { BeerDetailsComponent } from './beer-details/beer-details.component';

import { OrderByPipe } from './shared/pipes/orderby-pipe';

import { NgProgressModule } from 'ngx-progressbar';
import { NgxPaginationModule } from 'ngx-pagination';
import { BeerService } from './services/beer.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    OrderByPipe,
    BeersComponent,
    BeerDetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: BeersComponent, runGuardsAndResolvers: 'paramsOrQueryParamsChange' },
      { path: 'beers/:id', component: BeerDetailsComponent },
      { path: '**', redirectTo: 'home' }
    ]),
    NgProgressModule,
    NgxPaginationModule
  ],
  providers: [BeerService],
  bootstrap: [AppComponent]
})
export class AppModule { }
