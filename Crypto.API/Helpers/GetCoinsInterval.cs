using System.Threading.Tasks;
using Crypto.API.Models;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace Crypto.API.Helpers
{
    public class GetCoinsInterval
    {
        private static bool blnStarted { get; set; } = false;
        internal static CoinList coinlist { get; set; }
        private static System.Timers.Timer aTimer;
        internal async static Task<CoinList> GetCoinList()
        {
            // if (blnStarted == false)
            // { 
            //     coinlist = new CoinList();
                await Task.Run(() => coinlist.loadcoinMCapData()); 

            //     blnStarted = true;
            //     aTimer = new System.Timers.Timer(100000);  // every 100 seconds
            //     Timer();
            // }

            System.Console.WriteLine("called coinmarketcap !!!!!!!");
            System.Console.WriteLine("Delete this comment !!!!!!!");
            System.Console.WriteLine("Delete this comment !!!!!!!");
            System.Console.WriteLine("Delete this comment !!!!!!!");
            
            return coinlist;
        }

        internal async static Task<CoinList> GetCoinList_API()
        {
            try 
            {
                if (blnStarted == false)
                { 
                    blnStarted = true;
                    coinlist = new CoinList();
                    await Task.Run(() => coinlist.getCoinPrices_API()); 


                    aTimer = new System.Timers.Timer(300000);  // every 300 seconds / 5 mins
                    Timer();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

            return coinlist;
        }



        private static void Timer()
        {
                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = true;
                aTimer.Enabled = true;   
        }

        private async static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
           // await Task.Run(() => coinlist.loadcoinMCapData());
           await Task.Run(() => coinlist.getCoinPrices_API());            
        }


    }
}