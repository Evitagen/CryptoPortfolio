import { Component, OnInit, Input } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { RouterLink, Router } from '@angular/router';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-SelectPortfolio',
  templateUrl: './SelectPortfolio.component.html',
  styleUrls: ['./SelectPortfolio.component.css']
})
export class SelectPortfolioComponent implements OnInit {
  user: User;
  total: number;

  constructor(private userService: UserService, private authService: AuthService,
    private alertify: AlertifyService, private route: Router) { }

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
    }, error => {
      this.alertify.error(error);
    });
  }

  getValues() {
    this.total = 0.004;
  }

  viewPortfolio(portfolioID: number) {
    this.route.navigate(['/coinportfolio/' + portfolioID]);

    // console.log(portfolioID);
  }


  loggedIn() {
    return this.authService.loggedIn();
  }

}
