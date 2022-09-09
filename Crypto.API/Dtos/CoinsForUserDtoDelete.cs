using System.Collections.Generic;
using Crypto.API.Models;

namespace Crypto.API.Dtos
{
    public class CoinsForUsersDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public ICollection<CoinsForDetailedDto> coinHodles { get; set; }
    }
}