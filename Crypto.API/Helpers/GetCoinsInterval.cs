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



        internal static async Task<CoinList> GetCoinList_API(List<string> coinsInDb)
        {
            try 
            {
                if (blnStarted == false)
                { 
                    _coinlist = new CoinList();
                    _coinlist._CoinsInDB = coinsInDb;
                     blnStarted = true;
                     aTimer = new System.Timers.Timer(5000);  // every 300 seconds / 5 mins / 5000 = every 5 seconds
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
            if (_coinlist != null)
            {
                _coinlist = await Task.Run(() => _coinlist.getCoinPrices_APIAsync());
            }         
        }


    }
}