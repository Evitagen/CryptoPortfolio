import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval } from 'rxjs';

@Component({
  selector: 'app-coinlist',
  templateUrl: './coinlist.component.html',
  styleUrls: ['./coinlist.component.css']
})
export class CoinlistComponent implements OnInit, OnDestroy {
  coins: any;
  getprice = true;
  subscription: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
    this.pageRefresh();
  }

  ngOnDestroy() {
    console.log('destroy');
    this.subscription.unsubscribe();
  }

  getValues() {
    this.http.get('http://localhost:5000/api/coins').subscribe(Response => {
      this.coins = Response;
    }, error => {
      console.log(error);
    });
  }

  pageRefresh() {
   this.subscription = interval(100 * 60).subscribe(x => {
      this.getValues();
      console.log('heyheyhey');
    });
  }

}
