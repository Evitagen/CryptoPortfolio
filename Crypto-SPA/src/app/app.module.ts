import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { CoinlistComponent } from './coinlist/coinlist.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from 'src/app/_services/auth.service';

@NgModule({
   declarations: [
      AppComponent,
      CoinlistComponent,
      NavComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [
   AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
