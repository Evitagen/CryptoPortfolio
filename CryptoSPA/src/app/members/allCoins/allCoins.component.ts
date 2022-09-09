import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';
import { CoinsHodle } from 'src/app/_models/coinsHodle';
import { Portfolio } from 'src/app/_models/portfolio';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { interval } from 'rxjs';
import { HelperServiceService } from "./../../_services/HelperService.service";
//import { debug } from 'util';


@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-allCoins',
  templateUrl: './allCoins.component.html',
  styleUrls: ['./allCoins.component.css']
})
export class AllCoinsComponent implements OnInit, OnDestroy {
  user: User;
  total: number;
  coins: any;
  bitcoinPrice: number;
  AllcoinsList: CoinsHodle[] = [];
  AllcoinsnImageList: CoinsHodle[] = [];
  coinFound = false;
  subscription: any;
  coinhodleids: string = '';
  transactions: any;

  PieChart = [];

  constructor(private userService: UserService, private authService: AuthService,
    private alertify: AlertifyService, public helperService: HelperServiceService) { }

  async ngOnInit() {
    if (this.loggedIn) {
       this.loadUser(this.authService.decodedToken.nameid);
       await this.delay(1000);
      this.helperService.loadPieChart(this.AllcoinsList);
    }
    this.pageRefresh();
  }

  ngOnDestroy() {
    console.log('destroy');
    this.subscription.unsubscribe();
  }

   loadUser(id: number) {
    this.userService.getUser(id).subscribe((user: User) => {
      this.user = user;
      this.getCoinPrices();
      this.getValues(this.user);
    }, error => {
      this.alertify.error(error);
    });
  }

  pageRefresh() {
    this.subscription = interval(500 * 60).subscribe(async x => {
      if (this.loggedIn) {
        this.loadUser(this.authService.decodedToken.nameid);
        await this.delay(1000);
        this.helperService.loadPieChart(this.AllcoinsList);
      }
     });
   }

   getCoinPrices() {
    this.userService.getCoinPrices().subscribe(Response => {
      this.coins = Response;
      for (let i = 0; i < this.coins.length; i++) {
        if (this.coins[i].coinID === 'bitcoin') {
          this.bitcoinPrice = this.coins[i].price;
        }
      }
    }, error => {
      console.log(error);
    });
  }

  getValues(user: User) {

    this.AllcoinsnImageList = [];
    this.total = 0;
    this.AllcoinsList = [];
    this.coinFound = false;

    this.userService.getAllPortfolioCoins(user.id).subscribe((portfolios: Portfolio[]) => {


       this.coinhodleids = '';                                                                         //  gets all the coinhodle ids
       for (const portfolio of portfolios) {                                                      //  in all the portfolios
        if (portfolio && portfolio.coinsHodle.length > 0) {                                       //
          for (const co of portfolio.coinsHodle) {                                                //
             this.coinhodleids = this.coinhodleids + co.id + ',';                                  //
            this.coinFound = false;



            for (let i = 0; i < this.AllcoinsList.length; i++) {                                  // loop through each AllcoinsList
              if (co.coinID === this.AllcoinsList[i].coinID) {                                    // if the name matches
                this.coinFound = true;
              }
            }

            if (this.coinFound === false) {
              for (let i = 0; i < this.coins.length; i++) {
                if (co.coinID === this.coins[i].coinID) {
                  co.price = this.coins[i].price;
                  co.name = this.coins[i].name;
                }
              }
              this.AllcoinsList.push(co);                                                         // add coin and qty to Allcoins list
            }

          }                                                                                       //
        }                                                                                         //
       }                                                                                          //


       this.userService.getTransactions(this.coinhodleids).subscribe(async Response => {
        this.transactions = Response;
 
        for (let i = 0; i < this.AllcoinsList.length; i++) {
 
           for (const transaction of this.transactions) {
             if (transaction.coinsHodle.coinID === this.AllcoinsList[i].coinID) {
               this.AllcoinsList[i].quantity = this.AllcoinsList[i].quantity + transaction.amountBuy;     //  sets quantity
               this.AllcoinsList[i].quantity = this.AllcoinsList[i].quantity - transaction.amountSell;    //
             }
           }
 
        }


                // copy portfolio coins into coinsnImageList
                for (let i = 0; i < this.AllcoinsList.length; i++) {
                  const coinHodle = this.AllcoinsList[i];
                  this.AllcoinsnImageList.push(coinHodle);
                }
            
                for (let i = 0; i < this.AllcoinsnImageList.length; i++) {                                                  //
                  this.AllcoinsnImageList[i].imageLocation = 'assets/images/' + this.AllcoinsnImageList[i].coinID + '.png';   // images
                }

              for (const AllCoin of this.AllcoinsList) {                                //
                  if (AllCoin.price > 0 && AllCoin.quantity > 0) {                      // Totals 00000
                    this.total = this.total + (AllCoin.price * AllCoin.quantity);       //
                  }
              }

      });

         this.FormatNumbers();



    }, error => {
      this.alertify.error(error);
    });
  }

  FormatNumbers() {

    for (let i = 0; i < this.AllcoinsList.length; i++) {

      ///
      /// Format prices to correct decimal places.
      ///

      if (this.AllcoinsList[i].price < 1) {
        this.AllcoinsList[i].price = Number(this.AllcoinsList[i].price.toFixed(8));
      }

      if (this.AllcoinsList[i].price > 1 && this.AllcoinsList[i].price < 100) {
        this.AllcoinsList[i].price = Number(this.AllcoinsList[i].price.toFixed(4));
      }

      if (this.AllcoinsList[i].price > 100) {
        this.AllcoinsList[i].price = Number(this.AllcoinsList[i].price.toFixed(2));
      }

    }

  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }
}
