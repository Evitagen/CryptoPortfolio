import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

// const httpOptions = {
//   headers: new HttpHeaders({
//     'Authorization': 'Bearer ' + localStorage.getItem('token')
//   })
// };

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  deletethis: string;

  constructor(private http: HttpClient) {}

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'users');
  }

  getUser(id): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }

  updateUser(id: number, user: User) {
    return this.http.put(this.baseUrl + 'users/' + id, user);
  }

  getCoinPrices() {
    return this.http.get(this.baseUrl + 'coins');
  }

  getPCoinPrices_StringManip() {
    return this.http.get(this.baseUrl + 'Mcap/MarketCapString');
  }

  //
  // Portfolio
  //

  getPortfolioCoins(id: number) {
    return this.http.get(this.baseUrl + 'portfolio/' + id);
  }

  getAllPortfolioCoins(userId: number) {
    return this.http.get(this.baseUrl + 'portfolio/All/' + userId);
  }

  getTransactions(ids: string) {
    return this.http.get(this.baseUrl + 'portfolio/AllTran/' + ids);
  }



  addPortfolio(name: string, userId: number) {
    return this.http.post(this.baseUrl + 'portfolio/add/' + name + '/' + userId, {});
  }

  //
  //  Coins
  //

  addPortfolioCoin(coinid: number, portfolioId: number) {
    return this.http.post(this.baseUrl + 'portfolio/addcoin/' + coinid + '/' + portfolioId, {});
  }

  //
  // Transactions
  //

 addCoinTransaction(coin: string, coinhodleid: number, quantity: number, fee: number,
   date: string, priceWhenBoughtSold: number, coinid: number) {
    // tslint:disable-next-line:max-line-length
    // this.deletethis = this.baseUrl + 'portfolio/addtransaction/' + coin + '/' + portfolioId + '/' + quantity + '/' + fee + '/' + date + '/' + priceWhenBoughtSold;
    // tslint:disable-next-line:max-line-length

    return this.http.post(this.baseUrl + 'portfolio/addtransaction/' + coin + '/' + coinhodleid + '/' + quantity + '/' + fee + '/' + date + '/' + priceWhenBoughtSold + '/' + coinid, {});
  }





}
