using Crypto.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        

        public DbSet<User> Users { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<CoinsHodle> CoinsHodle {get; set;}
        public DbSet<Transactions> Transactions { get; set; }
      
    }
}