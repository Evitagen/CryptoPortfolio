import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';
import { CoinsHodle } from 'src/app/_models/coinsHodle';
import { Portfolio } from 'src/app/_models/portfolio';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { interval } from 'rxjs';
import { HelperServiceService } from "./../../_services/HelperService.service";
import { debug } from 'util';


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

  PieChart = [];

  constructor(private userService: UserService, private authService: AuthService,
    private alertify: AlertifyService, public helperService: HelperServiceService) { }

  async ngOnInit() {
    if (this.loggedIn) {
      this.loadUser(this.authService.decodedToken.nameid);
       await this.delay(400);
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
        await this.delay(400);
        this.helperService.loadPieChart(this.AllcoinsList);
      }
     });
   }

  getCoinPrices() {
    this.userService.getCoinPrices().subscribe(Response => {
      this.coins = Response;

      for (let i = 0; i < this.coins.length; i++) {
        if (this.coins[i].name === 'bitcoin') {
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

      for (const portfolio of portfolios) {                   // loop through each portfolio
        if (portfolio && portfolio.coinsHodle.length > 0) {   // if there are coins in that portfolio
          for (const co of portfolio.coinsHodle) {            // loop through each coin in that portfolio
            this.coinFound = false;

                for (let i = 0; i < this.AllcoinsList.length; i++) {                             // loop through each AllcoinsList
                  if (co.name === this.AllcoinsList[i].name) {                                   // if the name matches
                    this.coinFound = true;
                    this.AllcoinsList[i].quantity = this.AllcoinsList[i].quantity + co.quantity; // add quantity to existing in AllcoinsList

                        for (let j = 0; j < this.coins.length; j++) {                     // loop through each
                          if (co.name === this.coins[j].name) {                           //
                            this.AllcoinsList[i].price = this.coins[j].price;             //
                          }
                        }
                  }
                }

                if (this.coinFound === false) {       // if coin not found
                    for (let i = 0; i < this.coins.length; i++) {
                      if (co.name === this.coins[i].name) {
                        co.price = this.coins[i].price;
                      }
                    }
                    this.AllcoinsList.push(co);       // add coin and qty to Allcoins list
                }
          }
        }
      }


        // copy portfolio coins into coinsnImageList
        for (let i = 0; i < this.AllcoinsList.length; i++) {
          const coinHodle = this.AllcoinsList[i];
          this.AllcoinsnImageList.push(coinHodle);
        }
        // gets the icon location and puts in coinsnImageList
        for (let i = 0; i < this.AllcoinsnImageList.length; i++) {
          this.AllcoinsnImageList[i].imageLocation = 'assets/images/' + this.AllcoinsnImageList[i].name + '.png';
          // console.log(this.AllcoinsnImageList[i].imageLocation);
        }


      for (const AllCoin of this.AllcoinsList) {
          if (AllCoin.price > 0 && AllCoin.quantity > 0) {
            this.total = this.total + (AllCoin.price * AllCoin.quantity);
          }
      }

    }, error => {
      this.alertify.error(error);
    });
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }
}
