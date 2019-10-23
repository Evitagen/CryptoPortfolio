import { Component, OnInit, Output, EventEmitter, Directive, ElementRef, Input, HostListener } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { CoinsHodle } from 'src/app/_models/coinsHodle';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Transaction } from '../../_models/Transaction';
import { UserService } from 'src/app/_services/user.service';
import { Portfolio } from 'src/app/_models/portfolio';
import { moment } from 'ngx-bootstrap/chronos/test/chain';
import { formatDate } from '@angular/common';
import { BsDatepickerConfig } from 'ngx-bootstrap';


@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-newTransaction-modal',
  templateUrl: './newTransaction-modal.component.html',
  styleUrls: ['./newTransaction-modal.component.css']
})

export class NewTransactionModalComponent implements OnInit {

  TransactionModel = 'Buy';
  @Output() addTransaction = new EventEmitter();
  coin: CoinsHodle;
  transaction: Transaction;
  coinTransForm: FormGroup;
  // portfolio: Portfolio;
  // stringtransDate: string;
  transDate: Date;
  transDateISO: string;
  bsConfig: Partial<BsDatepickerConfig>;  // partial - makes all properties in type optional
  TotalSell: number;


  // public now: Date = new Date();

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder, private user: UserService) { }

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-blue'
    },
    this.createTransactionForm();
  }

  createTransactionForm() {

    var dateobj = new Date();
    function pad(n) {return n < 10 ? "0"+n : n;}
    var result = pad(dateobj.getDate())+"/"+pad(dateobj.getMonth()+1)+"/"+dateobj.getFullYear();

    this.coinTransForm = this.fb.group({
      QtyModel: ['', Validators.required],
      TransactionFee: ['0', Validators.required],
      dateTransaction: [ dateobj, [Validators.required, Validators.maxLength(10), Validators.minLength(10)]],
      PriceWhenBought: [this.coin.price, Validators.required]
    });
  }

  TransactionAdd() {
    if (this.coinTransForm.valid) {
        this.transaction = Object.assign({}, this.coinTransForm.value);

        const ddate = new Date(this.transaction.dateTransaction);

        this.transDateISO = ddate.toISOString();

        console.log(this.transaction.QtyModel);
        if (this.TransactionModel === 'Sell') {
          this.TotalSell = this.transaction.QtyModel - this.transaction.QtyModel - this.transaction.QtyModel;


          this.user.addCoinTransaction(this.coin.name, this.coin.id, this.TotalSell,
            this.transaction.TransactionFee, this.transDateISO, this.transaction.PriceWhenBought).subscribe(data => {
              console.log('success!!!!');

            }, error => {
              console.log('fail');
            });


        } else {

          this.user.addCoinTransaction(this.coin.name, this.coin.id, this.transaction.QtyModel,
            this.transaction.TransactionFee, this.transDateISO, this.transaction.PriceWhenBought).subscribe(data => {
              console.log('success!!!!');

            }, error => {
              console.log('fail');
            });

        }




      // emits more than 1 parameter
      // this.addTransaction.emit({Quantity: this.QtyModel, Fee: this.TransModel, Date: this.DateModel, PriceBought: this.PriceModel});
      this.addTransaction.emit(true);
      this.bsModalRef.hide();
    }
    console.log('done');

  }


}
