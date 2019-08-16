using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Crypto.API.Models;

namespace Crypto.API.Data
{
     class CryptoPrices 
    {
        public string strCoinMcap { get; set; }
        public List<coins> coins = new List<coins>();
        public List<int> indexOfCoinPrice = new List<int>();


        internal async Task LoadData()
        {
            strCoinMcap = await GetCoinMarketCapData();
        }

        internal List<coins> getPrices()
        {
           string strcoin = "";
           char c;
           int count = -1;
             
           indexOfCoinPrice = StringManipulation.AllIndexesOf(strCoinMcap,@"/#markets"" class=""price"" data-usd=");
           
           foreach (var item in indexOfCoinPrice)
           {
                coins coinToAdd = new coins("", 0);

                c = strCoinMcap[item + count];
                    while(c != '/')
                    {    
                        strcoin += c;
                        count --;
                        c = strCoinMcap[item + count];
                    }
                coinToAdd.Name = StringManipulation.Reverse(strcoin);
                coinToAdd.Price = GetPrice(StringManipulation.Reverse(strcoin));
                strcoin = "";
                count = -1;
                coins.Add(coinToAdd);
           }

            return coins;
        }

        internal decimal GetPrice(string strCoin)
        {
            return Math.Ceiling(decimal.Parse(StringManipulation.getBetween(strCoinMcap, @"<a href=""/currencies/" + strCoin + @"/#markets"" class=""price"" data-usd=""", @""" data-")) * 100) / 100;
        }

        internal async Task<String> GetCoinMarketCapData()
        {
            try
            {
                return await Task<string>.Run(async () =>
                {
                     return await FetchWebPage("https://coinmarketcap.com/");       // with await exceptions can only be caught when method returns result
                });                             
            }                                                                       
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }



        private static async Task<string> FetchWebPage(string url)
        {
            HttpClient httpClient = new HttpClient();
            return await httpClient.GetStringAsync(url);
        }

       
    }
}
