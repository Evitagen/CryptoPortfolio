using System;
using System.Collections.Generic;

namespace Crypto.API.Models
{
    public class others
    {
    public class Portfolio
    {
        public int PortfolioID { get; set; }
        public string PortfolioName { get; set; }
        public virtual List<Coin> coins{ get; set; }
    }

    public class Coin
    {
        public int CoinID { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }

        public virtual Portfolio portfolio { get; set; }
    }

    public class Transaction
    {
        public int TransactionID { get; set; }
        public DateTime datetime { get; set; }
        public decimal PricePaid { get; set; }
        public virtual Coin coin { get; set; }
    }

    public class PriceHistory
    {
        public int PriceHistoryID { get; set; }
        public decimal Price { get; set; }
        public DateTime datetime { get; set; }
        public virtual Coin coin { get; set; }
    }
    }
}