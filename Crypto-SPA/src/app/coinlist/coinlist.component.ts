import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { interval } from 'rxjs';

@Component({
  selector: 'app-coinlist',
  templateUrl: './coinlist.component.html',
  styleUrls: ['./coinlist.component.css']
})
export class CoinlistComponent implements OnInit {
  coins: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
    interval(100 * 60).subscribe(x => {
      this.getValues();
    });
  }

  getValues() {
    this.http.get('http://localhost:5000/api/coins').subscribe(Response => {
      this.coins = Response;
      // const cir = this.coins.circulatin;
      // const pri = this.coins.price;
      // this.coins.marketCap = cir + pri;
    }, error => {
      console.log(error);
    });
  }

}
