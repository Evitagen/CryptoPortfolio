import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval } from 'rxjs';
import { User } from 'src/app/_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Portfolio } from 'src/app/_models/portfolio';
import { BsModalService, TabHeadingDirective } from 'ngx-bootstrap';
import { CoinsHodle } from 'src/app/_models/coinsHodle';
import { NewTransactionModalComponent } from '../newTransaction-modal/newTransaction-modal.component';
import { CoinList } from 'src/app/_models/coinList';
import { AuthService } from 'src/app/_services/auth.service';
import { Chart } from 'chart.js';
import { CurrencyPipe } from '@angular/common';
import { HelperServiceService } from "./../../_services/HelperService.service";
import { exit } from 'process';
import { Transaction } from 'src/app/_models/Transaction';


@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-coinPortfolio',
  templateUrl: './coinPortfolio.component.html',
  styleUrls: ['./coinPortfolio.component.css']
})
export class CoinPortfolioComponent implements OnInit, OnDestroy {

  coins: any;
  images: string[];
  coinsLoaded = false;
  coinsDropDown: any;
  subscription: any;
  portfolio: Portfolio;
  transactions: any;
  bsModalRef: any;
  coinsnImageList: CoinsHodle[] = [];
  allowRefresh = true;

  PieChart = [];

  coinSelected: string;
  user: User;
  constructor(private http: HttpClient, private userService: UserService,
     private alertify: AlertifyService, private route: ActivatedRoute,
     private modalService: BsModalService, private authService: AuthService,
     public helperService: HelperServiceService) { }
  total: number;
  exists: Boolean = false;
  id: number;
  coinidselected: number;
  coinHodleids: string = '';
  totalcoinholdings: number = 0;


  async ngOnInit() {

    if (this.loggedIn) {
    // tslint:disable-next-line:radix
    this.id = parseInt(this.route.snapshot.paramMap.get('id'));
    this.loadPortfolio(this.id);

    // put delay
    await this.delay(50);
    this.getValues();
    // this.loadUsers(this.portfolio.userid);
    // this.pageRefresh();

    await this.delay(800);
    this.helperService.loadPieChart(this.coinsnImageList);
    }

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
    // this.subscription.unsubscribe();
  }

  getValues() {

    this.coinsnImageList = [];
    // Gets the total
    this.total = 0.00;


    this.userService.getCoinPrices().subscribe(async Response => {
    this.coins = Response;

    if (this.coinsLoaded === false) {
      this.coinsDropDown = this.coins;
      this.coinsLoaded = true;
    }


      // gets all transactions in portfolio
      this.coinHodleids = '';
      for (const co of this.portfolio.coinsHodle) {
          this.coinHodleids = this.coinHodleids + co.id + ',';
      }

      this.userService.getTransactions(this.coinHodleids).subscribe(async Response => {
          this.transactions = Response;


          // gets the total
          for (const coin of this.coins) {
            if (this.portfolio && this.portfolio.coinsHodle.length > 0) {
              for (const co of this.portfolio.coinsHodle) {

                if (co.coinID === coin.coinID) {

                  this.totalcoinholdings = 0;
                  co.price = coin.price;
                  co.name = coin.name;

                  for (const transaction of this.transactions) {             // get transactions loop through and add to get total holdings
                      if (transaction.coinsHodle.coinID === co.coinID) {

                        this.totalcoinholdings = this.totalcoinholdings + transaction.amountBuy;
                        this.totalcoinholdings = this.totalcoinholdings - transaction.amountSell;
                      }
                  }

                  co.quantity = this.totalcoinholdings;
                  if (co.price > 0 && this.totalcoinholdings > 0) {
                    this.total = this.total + (coin.price * this.totalcoinholdings);
                  }
                }
              }
            }
          }

      this.AddCoinImages();
      this.FormatNumbers();

    });

    }, error => {
      console.log(error);
    });
  }

  AddCoinImages() {

    this.coinsnImageList = [];

    // copy portfolio coins into coinsnImageList
      for (let i = 0; i < this.portfolio.coinsHodle.length; i++) {
        const coinHodle = this.portfolio.coinsHodle[i];
        this.coinsnImageList.push(coinHodle);
      }

    // gets the icon location and puts in coinsnImageList
    for (let i = 0; i < this.coinsnImageList.length; i++) {
      this.coinsnImageList[i].imageLocation = 'assets/images/' + this.coinsnImageList[i].name + '.png';
      // console.log(this.coinsnImageList[i].imageLocation);
    }

  }

  FormatNumbers() {

    for (let i = 0; i < this.coinsnImageList.length; i++) {

      ///
      /// Format prices to correct decimal places.
      ///

      if (this.coinsnImageList[i].price < 1) {
        this.coinsnImageList[i].price = Number(this.coinsnImageList[i].price.toFixed(8));
      }

      if (this.coinsnImageList[i].price > 1 && this.coinsnImageList[i].price < 100) {
        this.coinsnImageList[i].price = Number(this.coinsnImageList[i].price.toFixed(4));
      }

      if (this.coinsnImageList[i].price > 100) {
        this.coinsnImageList[i].price = Number(this.coinsnImageList[i].price.toFixed(2));
      }

    }

  }

  pageRefresh() {
   this.subscription = interval(1000 * 60).subscribe(x => {
      this.getValues();
    });
  }

  addTransaction(coin: CoinsHodle, portfolio: Portfolio) {

    const initialState = {
      coin,
      portfolio,
    };

    this.bsModalRef = this.modalService.show(NewTransactionModalComponent, {initialState});

    console.log('coin name ' + coin.name);

    this.bsModalRef.content.addTransaction.subscribe(async (values: string[]) => {

      console.log(values);
      debugger;
      this.bsModalRef.content.coin.quantity = values;

      // await this.delay(500);
      // this.loadPortfolio(this.id);
      // await this.delay(500);
      this.getValues();
      await this.delay(1000);
      this.helperService.loadPieChart(this.coinsnImageList);

    });

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

      // add coinid
      for (const coin of this.coins) {
        if (this.coinSelected == coin.name) {
          this.coinidselected = coin.coinID;
          break;
        }
      }

  
      this.userService.addPortfolioCoin(this.coinidselected, this.portfolio.portfolioID).subscribe(data => {
        this.alertify.success('Added ' + this.coinSelected);
        this.ngOnInit();
      }, error => {
        this.alertify.error(error);
      });
    } else {
      this.alertify.message('Please Select a Coin');
    }

  }

     delay(ms: number) {
      return new Promise( resolve => setTimeout(resolve, ms) );
    }

    loggedIn() {
      return this.authService.loggedIn();
    }

  }


