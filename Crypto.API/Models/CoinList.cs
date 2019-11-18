using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Crypto.API.Data;
using Crypto.API.Helpers;
using Newtonsoft.Json;

namespace Crypto.API.Models
{
    public class CoinList : IEnumerable
    {
        public static List<coins> coinlist = new List<coins>();

        public async void loadcoinMCapData()
        {
            CryptoPrices Prices = new CryptoPrices();
            await Prices.LoadData();
            coinlist = await Task.Run(() => Prices.getPricesAsync());
        }

        public void viewCoins()
        {
            foreach (var item in coinlist)
            {
                System.Console.WriteLine(item.Name);
            }
        }

        internal void getCoinPrices_API()
        {
        
            List<coins> coins = new List<coins>();

            var response = makeAPICall();

            System.Console.WriteLine(response);

            var CoinMCap = JsonConvert.DeserializeObject<RootObject>(response);

            coinlist.Clear();

            foreach (var item in CoinMCap.data)
            {
                coins coin = new coins();
                coin.Name = item.slug.ToString();    
                coin.Price = (decimal)item.quote.USD.price;
                coin.Volume = (decimal)item.quote.USD.volume_24h;
                coin.circulating = (decimal)item.circulating_supply;
                coin.Marketcap = (decimal)item.quote.USD.market_cap;
                coin.TotalSupply = (decimal)item.total_supply;
                coin.PercentChange1hr = (decimal)item.quote.USD.percent_change_1h;
                coin.PercentChange24hr = (decimal)item.quote.USD.percent_change_24h;
                coin.PercentChange7day = (decimal)item.quote.USD.percent_change_7d;
                coin.CoinMcapRank = item.cmc_rank;

                coins.Add(coin);
            }

            coinlist = coins;

        }

        static string makeAPICall()
        {
            // swap top (Live) with below (test)
            //var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            var URL = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "200";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();

            // swap top (Live) with below (test)
            //client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY_COINMCAP.API_KEY);
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY_COINMCAP.SANDBOX_API_KEY);


            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());
        }

        public IEnumerator GetEnumerator()
        {
            foreach (coins coin in coinlist)
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
        public double price { get; set; }
        public double volume_24h { get; set; }
        public double percent_change_1h { get; set; }
        public double percent_change_24h { get; set; }
        public double percent_change_7d { get; set; }
        public double market_cap { get; set; }
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