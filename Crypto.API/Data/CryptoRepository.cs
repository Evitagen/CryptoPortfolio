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
            var user = await _context.Users.Include(c => c.coinHodles).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public Task GetUser(object userid)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}