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
using System.Net.Http;

namespace Crypto.API.Models
{
    public class CoinList : IEnumerable
    {

        internal ICryptoRepository _repo;
        public List<coins> listofCoins = new List<coins>();
        public List<string> _CoinsInDB { get; set; }


        public async void loadcoinMCapData()
        {
            CryptoPrices Prices = new CryptoPrices();
            await Prices.LoadData();
            listofCoins = await Task.Run(() => Prices.getPricesAsync());
        }


        internal async Task<CoinList> getCoinPrices_APIAsync()
        {

            try
            {
                List<coins> coins = new List<coins>();

                //var responseC = makeAPICallCoinMarketCap();


                var responseG = makeAPICallCoinGecko();

                // System.Console.WriteLine(responseG);

                //var CoinMCap = JsonConvert.DeserializeObject<RootObject>(responseC);

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                var CoinGecko = JsonConvert.DeserializeObject<List<coinGeckojson>>(responseG, settings);


                listofCoins.Clear();

                // foreach (var item in CoinMCap.data)
                // {
                //     coins coin = new coins();
                //     coin.Name = item.slug.ToString();    
                //     if (!item.quote.USD.price.Equals(null)) { coin.Price = (decimal)item.quote.USD.price; } else { coin.Price = 0; }
                //     if (!item.quote.USD.volume_24h.Equals(null)) { coin.Volume = (decimal)item.quote.USD.volume_24h; } else { coin.Volume = 0; }
                //     if (!item.circulating_supply.Equals(null)) { coin.circulating = (decimal)item.circulating_supply; } else { coin.circulating = 0; }
                //     if (!item.quote.USD.market_cap.Equals(null)) { coin.Marketcap = (decimal)item.quote.USD.market_cap; } else { coin.Marketcap = 0; }
                //     if (!item.total_supply.Equals(null)) { coin.TotalSupply = (decimal)item.total_supply; } else { coin.TotalSupply = 0; }
                //     if (!item.quote.USD.percent_change_1h.Equals(null)) { coin.PercentChange1hr = (decimal)item.quote.USD.percent_change_1h; } else { coin.PercentChange1hr = 0; }
                //     if (!item.quote.USD.percent_change_24h.Equals(null)) { coin.PercentChange24hr = (decimal)item.quote.USD.percent_change_24h; } else { coin.PercentChange24hr = 0; }
                //     if (!item.quote.USD.percent_change_7d.Equals(null)) { coin.PercentChange7day = (decimal)item.quote.USD.percent_change_7d; } else { coin.PercentChange7day = 0; }
                //     coin.CoinMcapRank = item.cmc_rank;
                //     coin.CoinID = item.id;

                //     coins.Add(coin);
                // }

                foreach (var item in CoinGecko)
                {
                    coins coin = new coins();
                    coin.Name = item.name.ToString();
                    if (!item.current_price.Equals(null)) { coin.Price = item.current_price; } else { coin.Price = 0; }
                    if (!item.total_volume.Equals(null)) { coin.Volume = item.total_volume; } else { coin.Volume = 0; }
                    if (!item.circulating_supply.Equals(null)) { coin.circulating = item.circulating_supply; } else { coin.circulating = 0; }
                    if (!item.market_cap_change_24h.Equals(null)) { coin.Marketcap = item.market_cap_change_24h; } else { coin.Marketcap = 0; }
                    coin.TotalSupply = 0;
                    if (!item.price_change_percentage_1h_in_currency.Equals(null)) { coin.PercentChange1hr = item.price_change_percentage_1h_in_currency; } else { coin.PercentChange1hr = 0; }
                    if (!item.price_change_percentage_24h_in_currency.Equals(null)) { coin.PercentChange24hr = item.price_change_percentage_24h_in_currency; } else { coin.PercentChange24hr = 0; }
                    if (!item.price_change_percentage_7d_in_currency.Equals(null)) { coin.PercentChange7day = item.price_change_percentage_7d_in_currency; } else { coin.PercentChange7day = 0; }
                    if (!item.market_cap_rank.Equals(null)) { coin.CoinMcapRank = item.market_cap_rank; } else { coin.CoinMcapRank = 0; }
                    if (!item.id.Equals(null)) { coin.CoinID = item.id; } else { coin.CoinID = ""; }

                    coin.ImageUrl = item.image.ToString();
                    //Console.WriteLine(coin.ImageUrl); 
                    Console.WriteLine(item.id);
                    coins.Add(coin);
                }

                listofCoins = coins;


                // var coinsinDB = await _repo.GetCoinNamesList();
                await AddCoinName(coins);
                await AddPriceHistory(listofCoins);

                return this;

            }
            catch (Exception e)
            {
                using (StreamWriter writer = System.IO.File.AppendText("errorlog.txt"))
                {
                    writer.WriteLine("Error - " + e.ToString());
                }
                return null;
            }
        }




        ///
        /// if there are new coins in coinmarketcap list that don't exist then add them
        ///
        public async Task<bool> AddCoinName(List<coins> coinlist)
        {
            try
            {

                using (DbContext dbContextin = new DataContext())
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

                            string filePath = $@"/wwwroot/";

                            using (StreamWriter writer = System.IO.File.AppendText("wwwroot/assets/images/server.js"))
                            {
                                writer.WriteLine("downloadImageFromURL('" + coin_in_list.ImageUrl + "', '" + coin_in_list.CoinID + ".png');");
                            }

                            // this is now done in node - run server.js in the images folder
                            // downloadImageFromURL('https://assets.coingecko.com/coins/images/1/large/bitcoin.png?1547033579', 'bitcoin.png');

                            // using (WebClient webClient = new WebClient()) 
                            // {
                            //     byte [] data = webClient.DownloadData(coin_in_list.image);
                            // using (MemoryStream mem = new MemoryStream(data)) 
                            // {
                            //     using (var yourImage = Image.FromStream(mem)) 
                            //     { 
                            //         // If you want it as Png
                            //         yourImage.Save("/images/" + coin_in_list.coinID, ImageFormat.Png) ; 
                            //     }
                            // } 
                            // }

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


            using (DbContext dbContext = new DataContext())
            {
                foreach (var coin in coinlist)
                {
                    if (coin.CoinID == "bitcoin")
                    {
                        BTCPrice = coin.Price;
                    }
                    if (coin.CoinID == "ethereum")
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


        static string makeAPICallCoinGecko()
        {
            //   var URL = new UriBuilder("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=200&page=1&sparkline=false&price_change_percentage=1h%2C24h%2C7d");
            //   var client = new WebClient();
            //   return client.DownloadString(URL.ToString());

            // using var httpClient = new HttpClient();
            // var response = await httpClient.GetAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=200&page=1&sparkline=false&price_change_percentage=1h%2C24h%2C7d");
            // response.EnsureSuccessStatusCode();
            // return await response.Content.ReadAsStringAsync();

            var request = (HttpWebRequest)WebRequest.Create("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=200&page=1&sparkline=false&price_change_percentage=1h%2C24h%2C7d");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.UserAgent = "MyApplication/1.0"; // Set a custom user agent string

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseStream = response.GetResponseStream();
                using (var streamReader = new StreamReader(responseStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }


        static string makeAPICallCoinMarketCap()
        {

            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            // var URL = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            //var URL = new UriBuilder("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=200&page=1&sparkline=false");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "200";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();

            // swap top (Live) with below (test)    ////////////////////////////////////////////////////////////////////////////////////////////////  swap this one
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY_COINMCAP.API_KEY);
            // client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY_COINMCAP.API_KEY_TEST);
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


}