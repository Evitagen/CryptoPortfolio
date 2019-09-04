using Crypto.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        
        public DbSet<CoinsHodle> CoinsHodle {get; set;}
        public DbSet<User> Users { get; set; }
    }
}