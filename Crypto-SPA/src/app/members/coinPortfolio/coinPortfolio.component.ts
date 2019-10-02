import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval } from 'rxjs';
import { User } from 'src/app/_models/user';
import { UserService } from '../../_services/user.service';
// import { CoinsU } from '../../_models/coinsHodle';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Portfolio } from 'src/app/_models/portfolio';
import { CoinsHodle } from 'src/app/_models/coinsHodle';
import { load } from '@angular/core/src/render3/instructions';



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
  // coinU: CoinsU = { coinName: 'any', coinPrice: 0, coinQty: 0, priceBought: 0, PriceSold: 0 };
  // coinsU: CoinsU[];

  portfolio: Portfolio;


  user: User;
  users: User[];


  coinSelected: number;
  constructor(private http: HttpClient, private userService: UserService,
     private alertify: AlertifyService, private route: ActivatedRoute) { }
  total: number;

  ngOnInit() {


    // tslint:disable-next-line:radix
    const id = parseInt(this.route.snapshot.paramMap.get('id'));
    this.loadPortfolio(id);

    this.getValues();
    this.loadUsers();
    this.pageRefresh();
  }


  loadPortfolio(id: number) {
    this.userService.getPortfolioCoins(id).subscribe((portfolio: Portfolio) => {
      this.portfolio = portfolio;
    }, error => {
      this.alertify.error(error);
    });
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
          for (const co of this.portfolio.coinsHodle) {
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
        // this.coinU.coinName = coin.name;
        // this.coinU.coinPrice = coin.price;
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
