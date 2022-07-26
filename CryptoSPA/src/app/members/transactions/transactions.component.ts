import { Component, OnInit } from '@angular/core';
import { Portfolio } from 'src/app/_models/portfolio';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { User } from 'src/app/_models/user';
import { CoinsHodle } from 'src/app/_models/coinsHodle';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})

export class TransactionsComponent implements OnInit {

  user: User;
  coins: any;

  total: number;
  AllcoinsList: CoinsHodle[] = [];
  coinFound = false;
  coinSelected: string;
  portfolioSelected: string;


  constructor(private userService: UserService, private alertify: AlertifyService, private authService: AuthService) { }

  ngOnInit() {
    this.loadUser(this.authService.decodedToken.nameid);
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

  getCoinPrices() {
    this.userService.getCoinPrices().subscribe(Response => {
      this.coins = Response;
    }, error => {
      console.log(error);
    });
  }

  getValues(user: User) {

    this.total = 0;
    this.AllcoinsList = [];


    this.coinFound = false;

    const All: CoinsHodle = {
      id: 0,
      price: 0,
      name: '**** ALL ****',
      coinID: "",
      quantity: 0,
      portfolioID: 0,
      imageLocation: '',
      userId: 0
    };

     this.AllcoinsList.push(All);

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

    }, error => {
      this.alertify.error(error);
    });
  }

  onBtnView() {
    console.log('view');
  }

 
}
