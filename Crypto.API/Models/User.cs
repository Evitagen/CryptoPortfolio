using System.Collections.Generic;

namespace Crypto.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    
       // public ICollection<Coin> coin { get; set; } 
    }
}