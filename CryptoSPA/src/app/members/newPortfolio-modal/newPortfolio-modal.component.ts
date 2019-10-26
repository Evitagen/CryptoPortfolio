import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { User } from 'src/app/_models/user';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-newPortfolio-modal',
  templateUrl: './newPortfolio-modal.component.html',
  styleUrls: ['./newPortfolio-modal.component.css']
})
export class NewPortfolioModalComponent implements OnInit {
  NameModel = '';
  @Output() addPortfolio = new EventEmitter();


  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
  }

  PortfolioAdd() {
    this.addPortfolio.emit(this.NameModel);
    this.bsModalRef.hide();
  }

}
