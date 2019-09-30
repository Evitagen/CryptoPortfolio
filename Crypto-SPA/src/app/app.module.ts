import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap';
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { CoinlistComponent } from './coinlist/coinlist.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from 'src/app/_services/auth.service';
import { RegisterComponent } from './register/register.component';
import { CoindetailComponent } from './coindetail/coindetail.component';
import { appRoutes } from './routes';
import { CoinPortfolioComponent } from './members/coinPortfolio/coinPortfolio.component';


export function tokenGetter() {
   return localStorage.getItem('token');
  }

@NgModule({
   declarations: [
      AppComponent,
      CoinlistComponent,
      NavComponent,
      RegisterComponent,
      CoindetailComponent,
      CoinPortfolioComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      RouterModule.forRoot(appRoutes),
      BsDropdownModule.forRoot(),
      JwtModule.forRoot({
         config: {
           tokenGetter: tokenGetter,
           whitelistedDomains: ['localhost:5000'],
           blacklistedRoutes: ['localhost:5000/api/auth']
         }
       })
   ],
   providers: [
      AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
