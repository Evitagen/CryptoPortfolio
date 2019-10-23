import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval } from 'rxjs';
import { User } from 'src/app/_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Portfolio } from 'src/app/_models/portfolio';
import { BsModalService } from 'ngx-bootstrap';
import { CoinsHodle } from 'src/app/_models/coinsHodle';
import { NewTransactionModalComponent } from '../newTransaction-modal/newTransaction-modal.component';



@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-coinPortfolio',
  templateUrl: './coinPortfolio.component.html',
  styleUrls: ['./coinPortfolio.component.css']
})
export class CoinPortfolioComponent implements OnInit, OnDestroy {

  coins: any;
  coinsLoaded: boolean = false;
  coinsDropDown: any;

  subscription: any;
  portfolio: Portfolio;
  bsModalRef: any;
 



  coinSelected: string;
  user: User;
  constructor(private http: HttpClient, private userService: UserService,
     private alertify: AlertifyService, private route: ActivatedRoute,
     private modalService: BsModalService) { }
  total: number;
  exists: Boolean = false;
  id: number;

  ngOnInit() {


    // tslint:disable-next-line:radix
    this.id = parseInt(this.route.snapshot.paramMap.get('id'));
    this.loadPortfolio(this.id);
    this.getValues();
    // this.loadUsers(this.portfolio.userid);
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

    if (this.coinsLoaded === false) {
      this.coinsDropDown = this.coins;
      this.coinsLoaded = true;
    }

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

  addTransaction(coin: CoinsHodle, portfolio: Portfolio) {

    const initialState = {
      coin,
      portfolio
    };

    this.bsModalRef = this.modalService.show(NewTransactionModalComponent, {initialState});

    console.log('coin name ' + coin.name);

    this.bsModalRef.content.addTransaction.subscribe(async (values: string[]) => {

      // console.log('Quantity ' + values.Quantity);
      // console.log('Fee ' + values.Fee);
      // console.log('Date ' + values.Date);
      // console.log('PriceBought ' + values.PriceBought);

      await this.delay(1000);
      this.loadPortfolio(this.id);
      this.getValues();

    });

    // console.log('PortfolioId ' + coin.name);
    // console.log(coin.userId);
    // console.log(coin.name);
    // console.log(coin.price);
    // console.log(coin.quantity);


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

    // loadUsers(userId: number) {
    // this.userService.getUser(userId).subscribe((user: User) => {  // replace parameter with actual id
    //   this.user = user;
    //   console.log(user);
    // }, error => {
    //   this.alertify.error(error);
    // });

     delay(ms: number) {
      return new Promise( resolve => setTimeout(resolve, ms) );
    }

  }


