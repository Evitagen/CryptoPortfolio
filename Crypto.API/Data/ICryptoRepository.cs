using System.Threading.Tasks;
using Crypto.API.Models;

namespace Crypto.API.Data
{
    public interface ICryptoRepository
    {
         void Add<T>(T entity) where T: class;    // constrain where T is a type of class
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         
         //Task<IEnumerable<user>> GetUsers();
         Task<User> GetUser(int id);

         Task<CoinsHodle> GetCoinsHodle(int id);

         Task<Transactions> GetTransactions(int coinHodleId);

         Task<Portfolio> GetPortfolio(int id);

         Task<bool> AddPortfolio(string name, User user);

         Task<bool> AddCoinToPortfolio(string name, Portfolio portfolio);
         
         Task<bool> AddTransaction(CoinsHodle coinHodleID, decimal quantity, string datetime);

         Task<bool> UpdateCoinHodleAmount(CoinsHodle coinHodleID);

         Task GetUser(object userid);
    }
}