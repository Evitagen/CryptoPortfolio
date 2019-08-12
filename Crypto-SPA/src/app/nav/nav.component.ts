import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';



@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('logged in successfully');
      //this.alertify.success('Logged in successfully');
    }, error => {
      //this.alertify.error(error);
    }, () => {
      //this.router.navigate(['/members']);
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token; // !! retruns true if token has stuff and false if not
  }

  logout() {
    localStorage.removeItem('token');
    console.log('logged out');
  }

}
