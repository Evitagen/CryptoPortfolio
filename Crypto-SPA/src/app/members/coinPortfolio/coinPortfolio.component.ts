import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval } from 'rxjs';
import { User } from 'src/app/_models/user';
import { UserService } from '../../_services/user.service';
import { CoinsU } from '../../_models/coinsU';
import { AlertifyService } from '../../_services/alertify.service';



@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-coinPortfolio',
  templateUrl: './coinPortfolio.component.html',
  styleUrls: ['./coinPortfolio.component.css']
})
export class CoinPortfolioComponent implements OnInit, OnDestroy {

  coins: any;
  // getprice = true;
  subscription: any;
  coinU: CoinsU = { coinName: 'any', coinPrice: 0, coinQty: 0, priceBought: 0, PriceSold: 0 };
  coinsU: CoinsU[];
  user: User;
  users: User[];
  coinSelected: number;
  constructor(private http: HttpClient, private userService: UserService, private alertify: AlertifyService) { }
  total: number;

  ngOnInit() {
    this.getValues();
    this.loadUsers();
    this.pageRefresh();
  }

  ngOnDestroy() {
    console.log('destroy');
    this.subscription.unsubscribe();
  }

  getValues() {
    this.total = 0.004;
    this.userService.getCoinPrices().subscribe(Response => {
    this.coins = Response;
      for (const coin of this.coins) {
          for (const co of this.user.coinHodles) {
            if (co.name === coin.name) {
              co.price = coin.price;
              if (co.price > 0 && co.quantity > 0) {
                this.total = this.total + (coin.price * co.quantity);
              }
            }
          }
      }
    }, error => {
      console.log(error);
    });
  }

  pageRefresh() {
   this.subscription = interval(100 * 60).subscribe(x => {
      this.getValues();
    });
  }

  addTransaction() {

  }

  onBtnAdd() {
    // check coin doesn't already exist in list

    for (const coin of this.coins) {
      if (this.coinSelected === coin.name) {
        console.log('coin found');
        this.coinU.coinName = coin.name;
        this.coinU.coinPrice = coin.price;
       // this.coinsU.push(this.coinU);
      }
    }

   // console.log(this.coinSelected.name);
  }

    loadUsers() {
    this.userService.getUser(1).subscribe((user: User) => {  // replace parameter with actual id
      this.user = user;
      console.log(user);
    }, error => {
      this.alertify.error(error);
    });
  }

}
