import { Component, OnInit, Output, EventEmitter, Directive, ElementRef, Input, HostListener } from '@angular/core';


//import { BsModalRef } from 'ngx-bootstrap';
import { CoinsHodle } from 'src/app/_models/coinsHodle';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Transaction } from '../../_models/Transaction';
import { UserService } from 'src/app/_services/user.service';
import { Portfolio } from 'src/app/_models/portfolio';
import { moment } from 'ngx-bootstrap/chronos/test/chain';
import { formatDate } from '@angular/common';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { interval } from 'rxjs';



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
  TotalAfterTransaction: number;
  coins: any;





  // public now: Date = new Date();

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder, private user: UserService) { }

  isHidden = false;

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-green'
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

  async TransactionAdd() {

    this.isHidden = true;
   // await this.delay(1000);
    


    if (this.coinTransForm.valid) {

      this.user.getCoinPrices().subscribe(async Response => {
        this.coins = Response;


            this.transaction = Object.assign({}, this.coinTransForm.value);
            const ddate = new Date(this.transaction.dateTransaction);
            this.transDateISO = ddate.toISOString();

  

            if (this.TransactionModel === 'Sell') {                                                                           // Sell
              this.TotalSell = this.transaction.QtyModel - this.transaction.QtyModel - this.transaction.QtyModel;

              this.user.addCoinTransaction(this.coin.name, this.coin.id, this.TotalSell,
                this.transaction.TransactionFee, this.transDateISO, this.transaction.PriceWhenBought, this.coin.coinID).subscribe(data => {
                  this.TotalAfterTransaction = this.coin.quantity - this.transaction.QtyModel;
                  this.addTransaction.emit(this.TotalAfterTransaction);
                  console.log('success!!!!');

                }, error => {
                  console.log('fail');
                });


            } else {                                                                                                          // Buy

            this.user.addCoinTransaction(this.coin.name, this.coin.id, this.transaction.QtyModel,
                this.transaction.TransactionFee, this.transDateISO, this.transaction.PriceWhenBought, this.coin.coinID).subscribe(data => {
                this.TotalAfterTransaction = this.coin.quantity + this.transaction.QtyModel;
                this.addTransaction.emit(this.TotalAfterTransaction);
                console.log('success!!!!');
                

                }, error => {
                  console.log('fail');
                });

            }

          // emits more than 1 parameter
          // this.addTransaction.emit({Quantity: this.QtyModel, Fee: this.TransModel, Date: this.DateModel, PriceBought: this.PriceModel});
          
          this.bsModalRef.hide();
          
    });

    }
    console.log('done');
  }

  BuySellChange() {
   if (this.TransactionModel === 'Buy') {
    this.bsConfig = {
      containerClass: 'theme-green'
    };
   } else {
    this.bsConfig = {
      containerClass: 'theme-red'
    };
   }
  }


  
  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }


}
