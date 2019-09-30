import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval } from 'rxjs';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-coinlist',
  templateUrl: './coinlist.component.html',
  styleUrls: ['./coinlist.component.css']
})
export class CoinlistComponent implements OnInit, OnDestroy {
  coins: any;
  getprice = true;
  subscription: any;
  constructor(private userService: UserService) { }

  ngOnInit() {
    this.getValues();
    this.pageRefresh();
  }

  ngOnDestroy() {
    console.log('destroy');
    this.subscription.unsubscribe();
  }

  getValues() {
    this.userService.getCoinPrices().subscribe(Response => {
      this.coins = Response;
    }, error => {
      console.log(error);
    });
  }

  pageRefresh() {
   this.subscription = interval(100 * 60).subscribe(x => {
      this.getValues();
    });
  }

}
