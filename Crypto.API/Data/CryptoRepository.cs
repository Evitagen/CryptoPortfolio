using System.Threading.Tasks;
using Crypto.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.API.Data
{
    public class CryptoRepository : ICryptoRepository
    {
       
        private readonly DataContext _context;
        public CryptoRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
           _context.Remove(entity);
        }

        public async Task<CoinsHodle> GetCoinsHodle(int id)
        {
            var coinsHodle = await _context.CoinsHodle.FirstOrDefaultAsync(p => p.Id == id);
            return coinsHodle;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
            .Include(c => c.Portfolio)
            .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<Portfolio> GetPortfolio(int id)
        {
            var Portfolio = await _context.Portfolio
            .Include(c => c.coinsHodle)
            .FirstOrDefaultAsync(p => p.PortfolioID == id);

            return Portfolio;
        }

        public async Task<bool> AddPortfolio(string name, User user)
        {  
            Portfolio newPortfolio = new Portfolio();

            newPortfolio.PortfolioName = name;
            newPortfolio.User = user;

            _context.Portfolio.Add(newPortfolio);
             return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddCoinToPortfolio(string name, Portfolio portfolio)
        {
            CoinsHodle newCoinsHodle = new CoinsHodle();

            newCoinsHodle.Name = name;
            newCoinsHodle.Quantity = 0;
            newCoinsHodle.Portfolio = portfolio;

            _context.CoinsHodle.Add(newCoinsHodle);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<bool> AddPortfolio(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddPortfolio(string name, int id)
        {
            throw new System.NotImplementedException();
        }

        public Task GetUser(object userid)
        {
            throw new System.NotImplementedException();
        }
    }
}