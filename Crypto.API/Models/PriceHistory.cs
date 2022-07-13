using System;

namespace Crypto.API.Models
{
    public class PriceHistory
    {
        public int Id { get; set; }

        public string coinid { get; set; }

        public DateTime DateTime { get; set; }

        public decimal PriceUSD { get; set; }

        public decimal priceBTC { get; set; }

        public decimal priceETH { get; set; }

    }
}





// using System.Collections.Generic;

// namespace Crypto.API.Models
// {
//     public class CoinsHodle
//     {
//         public int Id { get; set; }
//         public string Name { get; set; }
//         public decimal Quantity { get; set; }   
//         public Portfolio Portfolio { get; set; }
//         public virtual ICollection<Transactions> Transactions { get; set; }
//     }
// }