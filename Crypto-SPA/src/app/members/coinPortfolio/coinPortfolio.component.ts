import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval } from 'rxjs';
import { User } from 'src/app/_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Portfolio } from 'src/app/_models/portfolio';
import { BsModalService } from 'ngx-bootstrap';



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


  // user: User;
  // users: User[];
  bsModalRef: any;


  coinSelected: string;
  user: User;
  constructor(private http: HttpClient, private userService: UserService,
     private alertify: AlertifyService, private route: ActivatedRoute,
     private modalService: BsModalService) { }
  total: number;
  exists: Boolean = false;

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

    // Gets the total

    this.total = 0.00;
    this.userService.getCoinPrices().subscribe(Response => {
    this.coins = Response;
      for (const coin of this.coins) {
        if (this.portfolio && this.portfolio.coinsHodle.length > 0) {
          for (const co of this.portfolio.coinsHodle) {
            if (co.name === coin.name) {
              co.price = coin.price;
              if (co.price > 0 && co.quantity > 0) {
                this.total = this.total + (coin.price * co.quantity);
              }
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


    this.exists = false;
    for (const coin of this.portfolio.coinsHodle) {
      if (this.coinSelected === coin.name) {
       this.alertify.warning('Already Exists');
       this.exists = true;
      }
    }

    if (this.exists === false && this.coinSelected != null) {
      this.userService.addPortfolioCoin(this.coinSelected, this.portfolio.portfolioID).subscribe(data => {
        this.alertify.success('Added ' + this.coinSelected);
        this.ngOnInit();
      }, error => {
        this.alertify.error(error);
      });
    } else {
      this.alertify.message('Please Select a Coin');
    }


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
