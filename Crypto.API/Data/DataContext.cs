using System.IO;
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
           // switch for production ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   Swap this to live
           //optionsBuilder.UseSqlServer("Server=localhost; Database=cryfolioapp4; User Id=sa; Password=Star1234;MultipleActiveResultSets=true");
           // optionsBuilder.UseSqlServer("Server=cryfolio.database.windows.net; Database=Cryfolio; User Id=appuser; Password=Star1234;MultipleActiveResultSets=true");     


            var server = "ms-sql-server";  // "ms-sql-server";    // put ip address of host
            var port = "1433";
            var user = "SA";
            var password = "1Secure*Password1";
            var database = "Cryfolio";

            optionsBuilder.UseSqlServer($"Server={server},{port};Database={database};User ID={user};Password={password};MultipleActiveResultSets=true");


            // optionsBuilder.UseMySql($"server=localhost; userid=root; pwd=secret; port=1433; database=cryfolioapp");


        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         IConfigurationRoot configuration = new ConfigurationBuilder()
        //         .SetBasePath(Directory.GetCurrentDirectory())
        //         .AddJsonFile("appsettings.Development.json")
        //         .Build();
        //         var connectionString = configuration.GetConnectionString("DbCoreConnectionString");
        //         optionsBuilder.UseSqlServer(connectionString);
        //     }
        // }
      
    }

}