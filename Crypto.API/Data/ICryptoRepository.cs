using System.Collections.Generic;
using System.Threading.Tasks;
using Crypto.API.Models;

namespace Crypto.API.Data
{
    public interface ICryptoRepository
    {
         void Add<T>(T entity) where T: class;    // constrain where T is a type of class
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         
    
         Task<User> GetUser(int id);

         Task<CoinsHodle> GetCoinsHodle(int id);

         Task<List<Transactions>> GetTransactions(string coinHodleIds);

         Task<Portfolio> GetPortfolio(int id);

         Task<List<Portfolio>> GetAllPortfolios(User user);

         Task<Transactions> GetTransactions(User user);


         Task<List<string>> GetCoinNamesList();



         Task<bool> AddPortfolio(string name, User user);

         Task<bool> AddCoinToPortfolio(int coinid, string coinidno, Portfolio portfolio);
         
         Task<bool> AddTransaction(CoinsHodle coinHodleID, decimal quantity, string datetime, decimal fee, decimal priceWhenBoughtSold, string coinid);

         Task<double> Get_Total_Coin_In_Portfolio(CoinsHodle coinHodleID, string coinid);
 
         Task<double> Get_Total_Coin_In_All_Portfolio(User user, string coinid);


        //  Task<bool> UpdateCoinHodleAmount(CoinsHodle coinHodleID);      // maybe get rid of this

        //  Task GetUser(object userid);
        
    }
}