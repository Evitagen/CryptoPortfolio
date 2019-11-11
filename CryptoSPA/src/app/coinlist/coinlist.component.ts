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
  coinsnImage: CoinList;
  coinImageLinks: string[] = [];
  coinsnImageList: CoinList[] = [];
  getprice = true;
  subscription: any;
  constructor(private userService: UserService) { }


  ngOnInit() {
    this.getValues();
    this.pageRefresh();
  }

  ngOnDestroy() {
    // console.log('destroy');
    this.subscription.unsubscribe();
  }

  getValues() {
    this.userService.getCoinPrices().subscribe((coins: CoinList) => {
      this.coins = coins;
      this.AddCoinImages();
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
      this.coinsnImageList[i].imagelocation = 'assets/images/' + this.coinsnImageList[i].name + '.png';
      console.log(this.coinsnImageList[i].imagelocation);
    }

  }

  pageRefresh() {
   this.subscription = interval(100 * 60).subscribe(x => {
      this.getValues();
    });
  }

}
