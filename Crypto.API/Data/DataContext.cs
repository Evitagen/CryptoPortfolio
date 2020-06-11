using System.IO;
using Crypto.API.Helpers;
using Crypto.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Crypto.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        

        public DbSet<User> Users { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<CoinsHodle> CoinsHodle {get; set;}
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<CoinNames> CoinNames { get; set; }
        public DbSet<PriceHistory> PriceHistory { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
  
            var server = API_KEY_COINMCAP.serverName; 
            var port = API_KEY_COINMCAP.port;
            var user = API_KEY_COINMCAP.user;
            var password = API_KEY_COINMCAP.password;
            var database = API_KEY_COINMCAP.database;


            optionsBuilder.UseSqlServer($"Server={server},{port};Database={database};User ID={user};Password={password};MultipleActiveResultSets=true");

        }

      
    }

}