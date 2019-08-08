using Crypto.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        
        public DbSet<Coin> Coins {get; set;}
    }
}