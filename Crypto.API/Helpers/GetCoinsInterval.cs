using System.Threading.Tasks;
using Crypto.API.Models;
using System;
using System.Timers;

namespace Crypto.API.Helpers
{
    public class GetCoinsInterval
    {
        private static bool blnStarted { get; set; } = false;
        internal static CoinList coinlist { get; set; }
        private static System.Timers.Timer aTimer;
        internal async static Task<CoinList> GetCoinList()
        {
            if (blnStarted == false)
            { 
                coinlist = new CoinList();
                await Task.Run(() => coinlist.loadcoinMCapData()); 

                blnStarted = true;
                aTimer = new System.Timers.Timer(10000);  // every 10 seconds
                Timer();
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
            await Task.Run(() => coinlist.loadcoinMCapData());              
        }


    }
}