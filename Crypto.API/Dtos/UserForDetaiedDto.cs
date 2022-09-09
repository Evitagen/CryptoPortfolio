using System.Collections.Generic;

namespace Crypto.API.Dtos
{
    public class UserForDetaiedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public ICollection<PortFoliosForDetailedDto> Portfolio { get; set; }
    }
}