using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Crypto.API.Data;
using Crypto.API.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Crypto.API.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Crypto.API.Models
{
    public class CoinList : IEnumerable
    {

        internal ICryptoRepository _repo;
        public List<coins> listofCoins = new List<coins>();
        public List<int> _CoinsInDB { get; set; }


        public async void loadcoinMCapData()
        {
            CryptoPrices Prices = new CryptoPrices();
            await Prices.LoadData();
            listofCoins = await Task.Run(() => Prices.getPricesAsync());
        }


        internal async Task<CoinList> getCoinPrices_APIAsync()
        {
                List<coins> coins = new List<coins>();

                var response = makeAPICall();

                System.Console.WriteLine(response);

                var CoinMCap = JsonConvert.DeserializeObject<RootObject>(response);

                listofCoins.Clear();

                foreach (var item in CoinMCap.data)
                {
                    coins coin = new coins();
                    coin.Name = item.slug.ToString();    
                    if (!item.quote.USD.price.Equals(null)) { coin.Price = (decimal)item.quote.USD.price; } else { coin.Price = 0; }
                    if (!item.quote.USD.volume_24h.Equals(null)) { coin.Volume = (decimal)item.quote.USD.volume_24h; } else { coin.Volume = 0; }
                    if (!item.circulating_supply.Equals(null)) { coin.circulating = (decimal)item.circulating_supply; } else { coin.circulating = 0; }
                    if (!item.quote.USD.market_cap.Equals(null)) { coin.Marketcap = (decimal)item.quote.USD.market_cap; } else { coin.Marketcap = 0; }
                    if (!item.total_supply.Equals(null)) { coin.TotalSupply = (decimal)item.total_supply; } else { coin.TotalSupply = 0; }
                    if (!item.quote.USD.percent_change_1h.Equals(null)) { coin.PercentChange1hr = (decimal)item.quote.USD.percent_change_1h; } else { coin.PercentChange1hr = 0; }
                    if (!item.quote.USD.percent_change_24h.Equals(null)) { coin.PercentChange24hr = (decimal)item.quote.USD.percent_change_24h; } else { coin.PercentChange24hr = 0; }
                    if (!item.quote.USD.percent_change_7d.Equals(null)) { coin.PercentChange7day = (decimal)item.quote.USD.percent_change_7d; } else { coin.PercentChange7day = 0; }
                    coin.CoinMcapRank = item.cmc_rank;
                    coin.CoinID = item.id;

                    coins.Add(coin);
                }

                listofCoins = coins;


                // var coinsinDB = await _repo.GetCoinNamesList();
                await AddCoinName(coins);
                await AddPriceHistory(listofCoins);

                return this;
        }




        ///
        /// if there are new coins in coinmarketcap list that don't exist then add them
        ///
        public async Task<bool> AddCoinName(List<coins> coinlist)
        {
            try
            {

                using(DbContext dbContextin = new DataContext())
                {     
                    var blnCoinExists = false;

                    foreach (var coin_in_list in coinlist)
                    {
                        blnCoinExists = false;

                        if (_CoinsInDB.Count > 0)
                        {
                            foreach (var coin_in_db in _CoinsInDB)
                            {
                                if (coin_in_db == coin_in_list.CoinID)
                                {
                                    blnCoinExists = true;
                                }
                            }
                        }


                        if (!blnCoinExists)
                        {
                            CoinNames coinNames = new CoinNames();
                            coinNames.Coinid = coin_in_list.CoinID;
                            coinNames.CoinName = coin_in_list.Name;   
                            dbContextin.Add(coinNames);
                            _CoinsInDB.Add(coin_in_list.CoinID);
                        }
                    } 

                return await dbContextin.SaveChangesAsync() > 0;
                } 

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                return true;
            }
        }

        public async Task<bool> AddPriceHistory(List<coins> coinlist)
        {
                decimal BTCPrice = 0;
                decimal ETHPrice = 0;


                using(DbContext dbContext = new DataContext())
                {
                foreach (var coin in coinlist)
                {
                    if (coin.CoinID == 1)
                    {
                        BTCPrice = coin.Price;
                    }
                    if (coin.CoinID == 1027)
                    {
                        ETHPrice = coin.Price;
                    }
                }

                foreach (var coin in coinlist)
                {
                    PriceHistory ph = new PriceHistory();
                    ph.coinid = coin.CoinID;
                    ph.DateTime = DateTime.Now;
                    ph.PriceUSD = coin.Price;
                    ph.priceBTC = Helpers.coinPriceConversions.priceinBTC(BTCPrice, coin.Price);
                    ph.priceETH = Helpers.coinPriceConversions.priceinEth(ETHPrice, coin.Price);
                    dbContext.Add(ph);
                }
                return await dbContext.SaveChangesAsync() > 0;
                }
        }

        static string makeAPICall()
        {

             var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            // var URL = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "200";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();

            // swap top (Live) with below (test)    ////////////////////////////////////////////////////////////////////////////////////////////////  swap this one
            // client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY_COINMCAP.API_KEY);
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY_COINMCAP.API_KEY_TEST);
            // client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY_COINMCAP.SANDBOX_API_KEY);


            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());
        }

        public IEnumerator GetEnumerator()
        {
            foreach (coins coin in listofCoins)
            {
                yield return coin;
            }
        }
    }







    public class coins
    {
        public coins()
        {
        }

        public coins(string name, decimal price)
        {
             this.Name = name;
             this.Price = price;
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal circulating { get; set; }
        public decimal Marketcap { get; set; }
        public decimal TotalSupply { get; set; }
        public decimal PercentChange1hr { get; set; }
        public decimal PercentChange24hr { get; set; }
        public decimal PercentChange7day { get; set; }
        public int CoinMcapRank { get; set; }
        public int CoinID { get; set; }
        public DateTime LastUpdated { get; set; }

    }

    public class Status
    {
        public DateTime timestamp { get; set; }
        public int error_code { get; set; }
        public object error_message { get; set; }
        public int elapsed { get; set; }
        public int credit_count { get; set; }
        public object notice { get; set; }
    }

    public class Platform
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string slug { get; set; }
        public string token_address { get; set; }
    }

    public class USD
    {
        public double? price { get; set; }
        public double? volume_24h { get; set; }
        public double? percent_change_1h { get; set; }
        public double? percent_change_24h { get; set; }
        public double? percent_change_7d { get; set; }
        public double? market_cap { get; set; }
        public DateTime last_updated { get; set; }
    }

    public class Quote
    {
        public USD USD { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string slug { get; set; }
        public int num_market_pairs { get; set; }
        public DateTime date_added { get; set; }
        public List<object> tags { get; set; }
        public double? max_supply { get; set; }
        public double circulating_supply { get; set; }
        public double total_supply { get; set; }
        public Platform platform { get; set; }
        public int cmc_rank { get; set; }
        public DateTime last_updated { get; set; }
        public Quote quote { get; set; }
    }

    public class RootObject
    {
        public Status status { get; set; }
        public List<Datum> data { get; set; }
    }

}