import { Component, OnInit, Input } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { RouterLink, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap';
import { NewPortfolioModalComponent } from '../newPortfolio-modal/newPortfolio-modal.component';
import { Portfolio } from 'src/app/_models/portfolio';
import { CoinsHodle } from 'src/app/_models/coinsHodle';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-SelectPortfolio',
  templateUrl: './SelectPortfolio.component.html',
  styleUrls: ['./SelectPortfolio.component.css']
})
export class SelectPortfolioComponent implements OnInit {
  user: User;
  total: number;
  bsModalRef: any;
  portfolio: Portfolio;

  AllcoinsList: CoinsHodle[] = [];
  coinFound = false;

  constructor(private userService: UserService, private authService: AuthService,
    private alertify: AlertifyService, private route: Router, private modalService: BsModalService) { }

  ngOnInit() {
    if (this.loggedIn) {
      this.loadUser(this.authService.decodedToken.nameid);
      // console.log(this.authService.decodedToken.nameid);
      // this.user = this.userService.getUser(this.authService.decodedToken.nameid);
    }
  }

   loadUser(id: number) {
    this.userService.getUser(id).subscribe((user: User) => {
      this.user = user;
      this.getValues(this.user);
    }, error => {
      this.alertify.error(error);
    });
  }

  getValues(user: User) {

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
                  }
                }

                if (this.coinFound === false) {       // if coin not found
                    this.AllcoinsList.push(co);       // add coin and qty to Allcoins list
                }
                
          }
        }
      }

      for (const AllCoin of this.AllcoinsList) {
        console.log(AllCoin);
      }

    }, error => {
      this.alertify.error(error);
    });
  }

  viewPortfolio(portfolioID: number) {
    this.route.navigate(['/coinportfolio/' + portfolioID]);
  }


  addPortfolio(user: User) {
    const initialState = {
      user
    };

    this.bsModalRef = this.modalService.show(NewPortfolioModalComponent, {initialState});

    this.bsModalRef.content.addPortfolio.subscribe((values) => {
      console.log(values);

      this.userService.addPortfolio(values, this.user.id).subscribe(data => {
        this.alertify.success('Added ' + values);
        this.loadUser(this.user.id);
      }, error => {
        this.alertify.error(error);
      });
    });
  }


  loggedIn() {
    return this.authService.loggedIn();
  }

}
