import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-coinlist',
  templateUrl: './coinlist.component.html',
  styleUrls: ['./coinlist.component.css']
})
export class CoinlistComponent implements OnInit {
  coins: any;

  constructor(private http:HttpClient) { }

  ngOnInit() {
    this.getValues();
  }
  
  getValues() {
    this.http.get('http://localhost:5000/api/values').subscribe(Response => {
      this.coins = Response;
    }, error => {
      console.log(error);
    });
  }

}
