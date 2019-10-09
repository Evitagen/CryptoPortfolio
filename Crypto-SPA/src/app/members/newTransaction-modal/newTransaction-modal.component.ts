import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-newTransaction-modal',
  templateUrl: './newTransaction-modal.component.html',
  styleUrls: ['./newTransaction-modal.component.css']
})
export class NewTransactionModalComponent implements OnInit {
  NameModel = '';
  @Output() addPortfolio = new EventEmitter();

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
  }

}
