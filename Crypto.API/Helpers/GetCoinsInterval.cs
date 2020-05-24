using System.Threading.Tasks;
using Crypto.API.Models;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Crypto.API.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Crypto.API.Helpers
{
    public class GetCoinsInterval
    {
        internal static bool blnStarted { get; set; } = false;
        public static CoinList _coinlist { get; set; }


        private static System.Timers.Timer aTimer;


       
        // public GetCoinsInterval(CoinList coinList, DataContext dc)
        // {
        //    // _repo = repo;
        //     _coinlist = coinList;
        //     // _dc = dc;

        // }

        // internal static async Task<CoinList> GetCoinList()
        // {
        //     await Task.Run(() => _coinlist.loadcoinMCapData()); 
        //     return _coinlist;
        // }

        internal static async Task<CoinList> GetCoinList_API(List<int> coins)
        {

            try 
            {
                if (blnStarted == false)
                { 
                    //  _CoinsInDB = await repo.GetCoinNamesList(); 
                    _coinlist = new CoinList();

           
                    _coinlist._CoinsInDB = coins;
            
                

                     blnStarted = true;
                     aTimer = new System.Timers.Timer(10000);  // every 300 seconds / 5 mins
                     Timer();
                    
                    
                    _coinlist = await Task.Run(() => _coinlist.getCoinPrices_APIAsync()); 
                   
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

            return _coinlist;
        }



        private static void Timer()
        {
                aTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e);
                aTimer.AutoReset = true;
                aTimer.Enabled = true;   
        }

        private static async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _coinlist = await Task.Run(() => _coinlist.getCoinPrices_APIAsync());
        }


    }
}