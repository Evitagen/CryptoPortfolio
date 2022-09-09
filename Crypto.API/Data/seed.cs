using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Crypto.API.Models;

namespace Crypto.API.Data
{
    public class seed
    {

        public static async System.Threading.Tasks.Task SeedCoinNameIDAsync(DataContext context)
        {
                // var coinlist = new CoinList();
                // await coinlist.getCoinPrices_APIAsync(); 

                // if (!context.CoinNames.Any())
                // {
                //     foreach (var item in coinlist.coinlist)
                //     {
                //         CoinNames coinNames = new CoinNames();
                //         coinNames.Coinid = item.CoinID;
                //         coinNames.CoinName = item.Name;   
                //         context.CoinNames.Add(coinNames);
                //     }
                    
                //     context.SaveChanges();
                // }
        }
    }
}