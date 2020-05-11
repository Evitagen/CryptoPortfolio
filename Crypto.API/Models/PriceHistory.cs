using System;

namespace Crypto.API.Models
{
    public class PriceHistory
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string coin { get; set; }
        public decimal Price { get; set; }

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