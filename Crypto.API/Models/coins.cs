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
        public string CoinID { get; set; }
        public string ImageUrl { get; set; }
        public DateTime LastUpdated { get; set; }

    }

    public class coinGecko_coins
    {
        public coinGecko_coins()
        {
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
        public string CoinID { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class coinGeckojson
    {
    public string id { get; set; }
    public string symbol { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public decimal current_price { get; set; }
    public double? market_cap { get; set; }
    public int market_cap_rank { get; set; }
    public double? fully_diluted_valuation { get; set; }
    public decimal total_volume { get; set; }
    public double? high_24h { get; set; }
    public double? low_24h { get; set; }
    public double? price_change_24h { get; set; }
    public double? price_change_percentage_24h { get; set; }
    public decimal market_cap_change_24h { get; set; }
    public double? market_cap_change_percentage_24h { get; set; }
    public decimal circulating_supply { get; set; }
    public double? total_supply { get; set; }
    public double? max_supply { get; set; }
    public double? ath { get; set; }
    public double? ath_change_percentage { get; set; }
    public DateTime ath_date { get; set; }
    public double? atl { get; set; }
    public double? atl_change_percentage { get; set; }
    public DateTime atl_date { get; set; }
    //public double? roi { get; set; }
    public DateTime last_updated { get; set; }
    public decimal price_change_percentage_1h_in_currency { get; set; }
    public decimal price_change_percentage_24h_in_currency { get; set; }
    public decimal price_change_percentage_7d_in_currency { get; set; }
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