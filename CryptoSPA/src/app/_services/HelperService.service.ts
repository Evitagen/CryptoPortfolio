import { Injectable } from '@angular/core';
import { CoinsHodle } from '../_models/coinsHodle';
import { CurrencyPipe } from '@angular/common';
import { Chart } from 'chart.js';

@Injectable({
  providedIn: 'root'
})
export class HelperServiceService {

constructor() { }

PieChart = [];

loadPieChart(coins: CoinsHodle[]) {

  let totals = new Array();
  let names = new Array();

                                                // red                    // yellow               // purple
  let backgroundcolorsPool = new Array('rgba(255, 99, 132, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(153, 102, 255, 0.2)', 
  'rgba(128, 0, 0, 0.2)', 'rgba(240, 128, 128, 0.2)', 'rgba(255, 215, 0, 0.2)',
  'rgba(240, 230, 140, 0.2)', 'rgba(0, 250, 154, 0.2)', 'rgba(0, 255, 255, 0.2)',
  'rgba(127, 215, 212, 0.2)', 'rgba(218, 112, 214, 0.2)', 'rgba(255, 182, 193, 0.2)');
  let bordercolorsPool = new Array('rgba(255, 99, 132, 1)', 'rgba(255, 206, 86, 1)', 'rgba(153, 102, 255, 1)',
   'rgba(128, 0, 0, 1)', 'rgba(240, 128, 128, 1)', 'rgba(255, 215, 0, 1)',
   'rgba(240, 230, 140, 1)', 'rgba(0, 250, 154, 1)', 'rgba(0, 255, 255, 1)',
   'rgba(127, 215, 212, 1)', 'rgba(218, 112, 214, 1)', 'rgba(255, 182, 193, 0.2)');
  let intArraycounter = 0;

  let backgroundColors = new Array();
  let borderColors = new Array();

  for (const coin of coins) {
    if ((coin.price * coin.quantity) > 0) {
      totals.push(coin.price * coin.quantity);
      names.push(coin.name);

      if (coin.name === 'bitcoin') {
        backgroundColors.push('rgba(255, 159, 64, 0.2)');
        borderColors.push('rgba(255, 159, 64, 1)');
      } else if (coin.name === 'litecoin' || coin.name === 'cardano') {
        backgroundColors.push('rgba(54, 162, 235, 0.2)');
        borderColors.push('rgba(54, 162, 235, 1)');
      } else if (coin.name === 'ethereum') {
        backgroundColors.push('rgba(192, 192, 192, 0.2)');
        borderColors.push('rgba(192, 192, 192, 1)');
      } else if (coin.name === 'bitcoin-cash') {
        backgroundColors.push('rgba(75, 192, 192, 0.2)');
        borderColors.push('rgba(75, 192, 192, 1)');
      } else if (coin.name === 'neo') {
        backgroundColors.push('rgba(124, 252, 0, 0.2)');
        borderColors.push('rgba(124, 252, 0, 1)');
      } else {
        backgroundColors.push(backgroundcolorsPool[intArraycounter]);
        borderColors.push(bordercolorsPool[intArraycounter]);
        intArraycounter += 1;
      }

    }
  }
debugger;
    this.PieChart = new Chart('pieChart', {
    type: 'pie',
    data: {
    labels: names,
    datasets: [{
      label: '# of coins',
        data: totals,
        backgroundColor: [
         backgroundColors[0],
         backgroundColors[1],
         backgroundColors[2],
         backgroundColors[3],
         backgroundColors[4],
         backgroundColors[5],
         backgroundColors[6],
        ],
        borderColor: [
          borderColors[0],
          borderColors[1],
          borderColors[2],
          borderColors[3],
          borderColors[4],
          borderColors[5],
          borderColors[6],
        ],
        borderWidth: 1
    }]
    },

    });

}





}
