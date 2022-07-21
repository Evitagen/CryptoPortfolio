import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BsDropdownModule, ModalModule, ButtonsModule } from 'ngx-bootstrap';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker'
import { JwtModule } from '@auth0/angular-jwt';


import { AppComponent } from './app.component';
import { CoinlistComponent } from './coinlist/coinlist.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from 'src/app/_services/auth.service';
import { RegisterComponent } from './register/register.component';
import { CoindetailComponent } from './coindetail/coindetail.component';
import { appRoutes } from './routes';
import { CoinPortfolioComponent } from './members/coinPortfolio/coinPortfolio.component';
import { SelectPortfolioComponent } from './members/SelectPortfolio/SelectPortfolio.component';
import { NewPortfolioModalComponent } from './members/newPortfolio-modal/newPortfolio-modal.component';
import { AuthGuard } from './_guards/auth.guard';
import { NewTransactionModalComponent } from './members/newTransaction-modal/newTransaction-modal.component';
import { AllCoinsComponent } from './members/allCoins/allCoins.component';
import { TransactionsComponent } from './members/transactions/transactions.component';

// export { BsDatepickerModule } from '../../node_modules/ngx-bootstrap/datepicker/bs-datepicker.module';

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
      CoinPortfolioComponent,
      SelectPortfolioComponent,
      NewPortfolioModalComponent,
      NewTransactionModalComponent,
      AllCoinsComponent,
      TransactionsComponent
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      ButtonsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      ModalModule.forRoot(),
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      JwtModule.forRoot({
         config: {
           tokenGetter: tokenGetter,
           whitelistedDomains: ['localhost:5000'],
           blacklistedRoutes: ['localhost:5000/api/auth']
         }
       })
   ],
   providers: [
      AuthService,
      AuthGuard
   ],
   entryComponents: [
      NewPortfolioModalComponent,
      NewTransactionModalComponent
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
