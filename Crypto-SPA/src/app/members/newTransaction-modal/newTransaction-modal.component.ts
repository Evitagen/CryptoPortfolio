import { Component, OnInit, Output, EventEmitter, Directive, ElementRef, Input, HostListener } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { CoinsHodle } from 'src/app/_models/coinsHodle';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Transaction } from '../../_models/Transaction';
import { UserService } from 'src/app/_services/user.service';
import { Portfolio } from 'src/app/_models/portfolio';
import { stringify } from '@angular/core/src/render3/util';
import { debug } from 'util';
import { moment } from 'ngx-bootstrap/chronos/test/chain';


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
  portfolio: Portfolio;
  stringtransDate: string;
  transDate: Date;
  transDateISO: string;

  public now: Date = new Date();

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder, private user: UserService) { }

  ngOnInit() {
    this.createTransactionForm();
  }

  createTransactionForm() {
    this.coinTransForm = this.fb.group({
      QtyModel: ['', Validators.required],
      TransactionFee: ['0', Validators.required],
      dateTransaction: [new Date().toLocaleDateString(), [Validators.required, Validators.maxLength(10), Validators.minLength(10)]],
      PriceWhenBought: [this.coin.price, Validators.required]
    });
  }

  TransactionAdd() {
    if (this.coinTransForm.valid) {
        this.transaction = Object.assign({}, this.coinTransForm.value);
        // console.log('adding stuff to db');
        // console.log('price bought = ' + this.transaction.PriceWhenBought);
        // console.log('qty = ' + this.transaction.QtyModel);
        // console.log('date = ' + this.transaction.dateTransaction);
        // console.log('Transaction fee = ' + this.transaction.TransactionFee);
        // console.log('Portfolio id = ' + this.portfolio.portfolioID);
        // console.log('coin - ' + this.coin.name);
        // console.log('coinhodleID = ' + this.coin.id);

       const momentVariable = moment(this.transaction.dateTransaction.toString(), 'DD-MM-YYYY');  // gets uk date into moment
       const stringvalue = momentVariable.format('YYYY-MM-DD');                                   // formats the best way
       this.transDate = new Date(stringvalue);                                                    // turns back into date
       this.transDateISO = this.transDate.toISOString();                                          // turns to iso string


        this.user.addCoinTransaction(this.coin.name, this.coin.id, this.transaction.QtyModel,
           this.transaction.TransactionFee, this.transDateISO, this.transaction.PriceWhenBought).subscribe(data => {
             console.log('success!!!!');

           }, error => {
             console.log('fail');
           });

      // emits more than 1 parameter
      // this.addTransaction.emit({Quantity: this.QtyModel, Fee: this.TransModel, Date: this.DateModel, PriceBought: this.PriceModel});
      this.addTransaction.emit(true);
      this.bsModalRef.hide();
    }
    console.log('done');

  }


}