using System.Collections.Generic;

namespace Crypto.API.Models
{
    public class CoinsHodle
    {
        public int Id { get; set; }
        
        // public string Name { get; set; }
        public decimal Quantity { get; set; } 
        public string coinID { get; set; }  
        public Portfolio Portfolio { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}