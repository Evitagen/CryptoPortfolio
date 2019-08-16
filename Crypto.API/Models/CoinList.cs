using System.Collections;
using System.Collections.Generic;
using Crypto.API.Data;

namespace Crypto.API.Models
{
    public class CoinList : IEnumerable
    {

        public static List<coins> coinlist = new List<coins>();


        public async void loadcoinMCapData()
        {
            CryptoPrices Prices = new CryptoPrices();

            await Prices.LoadData();

            coinlist = Prices.getPrices();

      
            //coinlist.Clear();

            // if (Prices.strCoinMcap.Length > 0)
            // {
            //     var dblBTCPrice = Prices.GetPrice("bitcoin");
            //     var dblEtherPrice = Prices.GetPrice("ethereum");
            //     var dblLiteCoin = Prices.GetPrice("litecoin");

        
            //     coinlist.Clear();

            //     coins b = new coins("Bitcoin", dblBTCPrice);
            //     coins l = new coins("Litecoin", dblLiteCoin);
            //     coins e = new coins("Ethereum", dblEtherPrice);

            //     coinlist.Add(b);
            //     coinlist.Add(l);
            //     coinlist.Add(e);
            // }
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
        public coins(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}