using System.Collections.Generic;

namespace Crypto.API.Dtos
{
    public class PortFoliosForDetailedDto
    {
        public string PortfolioID { get; set; }
        public string PortfolioName { get; set; }
        public ICollection<CoinsForDetailedDto> Coins { get; set; }
    }
}