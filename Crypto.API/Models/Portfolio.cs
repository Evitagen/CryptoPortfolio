using System.Collections.Generic;

namespace Crypto.API.Models
{
    public class Portfolio
    {
        public int PortfolioID { get; set; }
        public string PortfolioName { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CoinsHodle> coinsHodle{ get; set; }
    }
}