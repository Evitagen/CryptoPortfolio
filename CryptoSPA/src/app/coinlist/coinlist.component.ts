import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval } from 'rxjs';
import { UserService } from '../_services/user.service';
import { CoinList } from 'src/app/_models/coinList';

@Component({
  selector: 'app-coinlist',
  templateUrl: './coinlist.component.html',
  styleUrls: ['./coinlist.component.css']
})
export class CoinlistComponent implements OnInit, OnDestroy {
  coins: CoinList;
  coinsPrev: CoinList;
  coinsnImage: CoinList;
  // coinImageLinks: string[] = [];
  coinsnImageList: CoinList[] = [];
  getprice = true;
  subscription: any;
  constructor(private userService: UserService) { }


  ngOnInit() {
    this.getValues();
    this.pageRefresh();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getValues() {
    debugger;
    this.coinsPrev = this.coins;
    this.userService.getCoinPrices().subscribe((coins: CoinList) => {
      this.coins = coins;
      this.AddCoinImages();
      this.FormatNumbers();
      this.CompareBullBear();
    }, error => {
      console.log(error);
    });
  }

  getValuesCoinMCap_StringManip() {
    this.userService.getPCoinPrices_StringManip().subscribe((coins: CoinList) => {
      this.coins = coins;
      this.AddCoinImages();
    }, error => {
      console.log(error);
    });
  }

  AddCoinImages() {
    this.coinsnImageList = [];

    for (let i = 0; i < this.coins.length; i++) {
      const coin = this.coins[i];
      this.coinsnImageList.push(coin);
    }

    for (let i = 0; i < this.coinsnImageList.length; i++) {
      this.coinsnImageList[i].imagelocation = 'assets/images/' + this.coinsnImageList[i].coinID + '.png';
      console.log(this.coinsnImageList[i].imagelocation);
    }

  }

  FormatNumbers() {

    for (let i = 0; i < this.coinsnImageList.length; i++) {

      ///
      /// Format prices to correct decimal places.
      ///

      if (this.coinsnImageList[i].price < 1) {
        this.coinsnImageList[i].price = Number(this.coinsnImageList[i].price.toFixed(6));
      }

      if (this.coinsnImageList[i].price > 1 && this.coinsnImageList[i].price < 100) {
        this.coinsnImageList[i].price = Number(this.coinsnImageList[i].price.toFixed(4));
      }

      if (this.coinsnImageList[i].price > 100) {
        this.coinsnImageList[i].price = Number(this.coinsnImageList[i].price.toFixed(2));
      }

      ///
      /// Format Percentage changed to 2dp
      ///

      this.coinsnImageList[i].percentChange1hr = Number(this.coinsnImageList[i].percentChange1hr.toFixed(2));
      this.coinsnImageList[i].percentChange24hr = Number(this.coinsnImageList[i].percentChange24hr.toFixed(2));
      this.coinsnImageList[i].percentChange7day = Number(this.coinsnImageList[i].percentChange7day.toFixed(2));

    }

  }

  CompareBullBear() {
    for (let i = 0; i < this.coins.length; i++) {
        for (let j = 0; j < this.coinsPrev.length; j++) {
            if (this.coinsPrev.price > this.coins.price) {
              this.coins.prevPriceHigherLowerSame = 1;
            }
            else if (this.coinsPrev.price < this.coins.price) {
              this.coins.prevPriceHigherLowerSame = 2;
            }
            else if (this.coinsPrev.price === this.coins.price) {
              this.coins.prevPriceHigherLowerSame = 0;
            }        
        }
    }
  }

  pageRefresh() {
   this.subscription = interval(100 * 60).subscribe(x => {
      this.getValues();
    });
  }

}
