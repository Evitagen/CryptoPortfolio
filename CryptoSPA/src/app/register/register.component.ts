import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('registration success');
      this.alertify.success('registration success');
      this.router.navigate(['/coinlist']);
    }, error => {
      console.log(error);
      this.alertify.error('registration failed');
    });
  }

  cancel() {
    this.router.navigate(['/coinlist']);
  }

}
