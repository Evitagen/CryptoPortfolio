import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { CoinlistComponent } from './coinlist/coinlist.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from 'src/app/_services/auth.service';
import { RegisterComponent } from './register/register.component';
import { CoindetailComponent } from './coindetail/coindetail.component';
import { appRoutes } from './routes';


@NgModule({
   declarations: [
      AppComponent,
      CoinlistComponent,
      NavComponent,
      RegisterComponent,
      CoindetailComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
