using System;

namespace Crypto.API.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double AmountBuy { get; set; }
        public double AmountSell { get; set; }
        public string Coinid { get; set; }
        public decimal fee { get; set; }
        public decimal priceWhenBoughtSold { get; set; }
        public virtual CoinsHodle CoinsHodle{ get; set; }

    }
}