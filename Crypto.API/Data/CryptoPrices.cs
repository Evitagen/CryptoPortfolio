using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Crypto.API.Models;
using System.Threading;

namespace Crypto.API.Data
{
     class CryptoPrices 
    {
        public string strCoinMcap { get; set; }
        public List<coins> coins = new List<coins>();
        public List<int> indexOfCoinPrice = new List<int>();
        public List<int> indexOfCirculatingSup = new List<int>();

        internal async Task LoadData()
        {
            strCoinMcap = await GetCoinMarketCapData();
        }

        internal List<coins> getPrices()
        {
           string strcoin = "";
           char c;
           int count = -1;
             
           try
           {
                indexOfCoinPrice = StringManipulation.AllIndexesOf(strCoinMcap,@"/#markets"" class=""price"" data-usd=");
                indexOfCirculatingSup = StringManipulation.AllIndexesOf(strCoinMcap,@"circulating-supply");

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
                        strcoin = StringManipulation.Reverse(strcoin);
                        coinToAdd.Name = strcoin;
                        Task.Run(() => coinToAdd.Volume = getVolume(strcoin));            
                         coinToAdd.Price = GetPrice(strcoin);   
                        coinToAdd.circulating = getCirculatingSuply(strcoin);  
                        //coinToAdd.Marketcap = GetCoinMCap(coinToAdd);                   
                        strcoin = "";
                        count = -1;
                        coins.Add(coinToAdd);
                    }

                return coins;
           }
           catch (Exception)
           {
               return null;
           }
        
        }

        internal decimal GetPrice(string strCoin)
        {
            try
            {
                return Math.Ceiling(decimal.Parse(StringManipulation.getBetween(strCoinMcap, @"<a href=""/currencies/" + strCoin + @"/#markets"" class=""price"" data-usd=""", @""" data-")) * 100) / 100;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return 0;
            }   
        }

        internal decimal getVolume(string strCoin)
        {    
            string strvolume = "";
            int count = 0;
            int index1 = strCoinMcap.IndexOf(strCoin + @"/#markets"" class=""volume"" data-usd=""");
            int intCoinLength = strCoin.Length;
            index1 = index1 + 36 + intCoinLength;
            char c = strCoinMcap[index1];
                while(c != '"')
                {    
                    strvolume += c;
                    count ++;
                    c = strCoinMcap[index1 + count];
                }
            return decimal.Parse(strvolume);
        }


        
        internal decimal getCirculatingSuply(string strCoin)
        {
            try
            {
                 StringBuilder strCirculating = new StringBuilder();
            int count = 0;
            int indexCoinCirculating = 0;
            int indexCoin = strCoinMcap.IndexOf(strCoin + @"/#markets"" class=""volume"" data-usd=""");

              foreach (int item in indexOfCirculatingSup)
              {
                  if (item > indexCoin)
                  {
                      indexCoinCirculating = item;
                      break;
                  }
              }
              
              indexCoinCirculating += 31;
              char c = strCoinMcap[indexCoinCirculating];
              while(c != '"')
              {
               strCirculating.Append(c);   
               count ++;
               c = strCoinMcap[indexCoinCirculating + count];
               
              }
               return decimal.Parse(strCirculating.ToString());

            }
            catch (Exception ex)
            {
                return 0;
            }
           
        }

        internal decimal GetCoinMCap(coins coinToAdd)
        {
            decimal decReturn = 0;

            if (coinToAdd.circulating > 0 && coinToAdd.Price > 0)
            {
                decReturn = coinToAdd.circulating * coinToAdd.Price;
            }

            return decReturn;
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
