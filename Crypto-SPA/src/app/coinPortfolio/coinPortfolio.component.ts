import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval } from 'rxjs';
import { User } from 'src/app/_models/user';
import { UserService } from '../_services/user.service';
import { CoinsU } from '../_models/coinsU';
import { AlertifyService } from '../_services/alertify.service';


@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-coinPortfolio',
  templateUrl: './coinPortfolio.component.html',
  styleUrls: ['./coinPortfolio.component.css']
})
export class CoinPortfolioComponent implements OnInit, OnDestroy {

  coins: any;
  getprice = true;
  subscription: any;
  coinU: CoinsU = { coinName: 'any', coinPrice: 0, coinQty: 0, priceBought: 0, PriceSold: 0 };
  coinsU: CoinsU[];
  user: User;
  users: User[];
  coinSelected: number;
  constructor(private http: HttpClient, private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.getValues();
   // this.loadUsers();
    this.pageRefresh();
  }

  ngOnDestroy() {
    console.log('destroy');
    this.subscription.unsubscribe();
  }

  getValues() {
    this.http.get('http://localhost:5000/api/coins').subscribe(Response => {
      this.coins = Response;
    }, error => {
      console.log(error);
    });
  }

  pageRefresh() {
   this.subscription = interval(100 * 60).subscribe(x => {
      this.getValues();
    });
  }

  onBtnAdd() {
    // check coin doesn't already exist in list

    for (const coin of this.coins) {
      if (this.coinSelected == coin.name)
      {
        console.log('coin found');
        this.coinU.coinName = coin.name;
        this.coinU.coinPrice = coin.price;
       // this.coinsU.push(this.coinU);
      }
    }

   // console.log(this.coinSelected.name);
  }

    // loadUsers() {
    // this.userService.getUsers().subscribe((users: User[]) => {
    //   this.users = users;
    // }, error => {
    //   this.alertify.error(error);
    // });
  //}

}
