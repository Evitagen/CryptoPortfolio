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

  getPortfolioCoins(id: number) {
    return this.http.get(this.baseUrl + 'portfolio/' + id);
  }

  addPortfolio(name: string, userId: number) {
    return this.http.post(this.baseUrl + 'portfolio/add/' + name + '/' + userId, {});
  }

   addPortfolioCoin(coin: string, portfolioId: number) {
     return this.http.post(this.baseUrl + 'portfolio/addcoin/' + coin + '/' + portfolioId, {});
   }

}
