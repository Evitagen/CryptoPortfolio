/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CoinlistComponent } from './coinlist.component';

describe('CoinlistComponent', () => {
  let component: CoinlistComponent;
  let fixture: ComponentFixture<CoinlistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CoinlistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CoinlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
