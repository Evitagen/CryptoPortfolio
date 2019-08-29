using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crypto.API.Data;

namespace Crypto.API.Models
{
    public class CoinList : IEnumerable
    {
        public static List<coins> coinlist = new List<coins>();
        public string CoinListName { get; set; }    // used for different user or different portfolios
        public int CoinListID { get; set; }

        public async void loadcoinMCapData()
        {
            CryptoPrices Prices = new CryptoPrices();
            await Prices.LoadData();
            coinlist = await Task.Run(() => Prices.getPrices());
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
        public decimal Volume { get; set; }
        public decimal circulating { get; set; }
        public decimal Marketcap { get; set; }

    }
}