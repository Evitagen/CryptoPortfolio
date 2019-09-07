namespace Crypto.API.Models
{
    public class CoinsHodle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }   
        public User User { get; set; }
        public int UserId { get; set; }
    }
}