using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypto.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.API.Data
{
    public class CryptoRepository : ICryptoRepository
    {
       
        private DataContext _context;
        public CryptoRepository(DataContext context)
        {
            _context = context;
        }

        public CryptoRepository()
        {
            
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
            var coinsHodle = await _context.CoinsHodle
            .Include(t => t.Transactions)
            .FirstOrDefaultAsync(p => p.Id == id);
            return coinsHodle;
        }

        public async Task<Transactions> GetTransactions(int coinHodleId)
        {
            var transactions = await _context.Transactions.FirstOrDefaultAsync(p => p.CoinsHodle.Id == coinHodleId);
            return transactions;
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

        public async Task<List<Portfolio>> GetAllPortfolios(User user)
        {
            //Portfolio[] portfolios = new Portfolio[] {};
            List<Portfolio> portfolios = new List<Portfolio>();

            foreach (var portfolio in user.Portfolio)
            {
              portfolios.Add(await GetPortfolio(portfolio.PortfolioID));
            }

            //  var Portfolio = await _context.Portfolio
            // .Include(c => c.coinsHodle)
            // .FirstOrDefaultAsync(p => p.User.Id == Userid);

            return portfolios;
        }

        public async Task<bool> AddPortfolio(string name, User user)
        {  
            Portfolio newPortfolio = new Portfolio();

            newPortfolio.PortfolioName = name;
            newPortfolio.User = user;

            _context.Portfolio.Add(newPortfolio);
             return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddCoinToPortfolio(int coinid, string name, Portfolio portfolio)
        {
            CoinsHodle newCoinsHodle = new CoinsHodle();

            newCoinsHodle.Name = name;
            newCoinsHodle.coinID = coinid;
            newCoinsHodle.Quantity = 0;
            newCoinsHodle.Portfolio = portfolio;

        

            _context.CoinsHodle.Add(newCoinsHodle);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddTransaction(CoinsHodle coinHodleID, decimal quantity, string datetime)
        {

            
            Transactions transaction = new Transactions();
         
           
            //DateTime d2 = DateTime.Parse("2010-08-20T15:00:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind);
            transaction.Date = DateTime.Parse(datetime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            
            if (decimal.ToDouble(quantity) > 0)
            {
                transaction.AmountSell = 0;
                transaction.AmountBuy = decimal.ToDouble(quantity);
            }
            else
            {
                 transaction.AmountBuy = 0 ;
                 transaction.AmountSell = decimal.ToDouble(quantity) - decimal.ToDouble(quantity) - decimal.ToDouble(quantity);
            }
           
           transaction.CoinsHodle = coinHodleID;
           
            if (transaction.AmountBuy != 0 || transaction.AmountSell != 0)
            {
                _context.Transactions.Add(transaction);
            }

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdateCoinHodleAmount(CoinsHodle coinHodleID)
        {

                double totalBuy = 0;
                double totalSell = 0;
                double total = 0;

                foreach (var coinTrans in coinHodleID.Transactions)
                {
                    totalBuy += coinTrans.AmountBuy;
                    totalSell += coinTrans.AmountSell;      
                }
                total = totalBuy - totalSell;

                coinHodleID.Quantity = Convert.ToDecimal(total);

                _context.CoinsHodle.Update(coinHodleID);

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

        public Task<Transactions> GetTransactions(User user)
        {
            throw new NotImplementedException();
        }





        public async Task<List<int>> GetCoinNamesList()
        {
            var coinsList = new List<int>();

            try
            {
                var coins_In_Database =  _context.CoinNames.ToList();
                coinsList.Clear();
                foreach (var item in coins_In_Database)
                {
                    coinsList.Add(item.Coinid);
                }       
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
             return coinsList;
            }

        return coinsList;

        }

        // public async Task<bool> AddCoinName(List<coins> coinlist)
        // {

        //     var coins_In_Database = _context.CoinNames;
        //     var blnCoinExists = false;

        //     foreach (var coin_in_list in coinlist)
        //     {
        //         blnCoinExists = false;

        //         foreach (var coin_in_db in coins_In_Database)
        //         {
        //             if (coin_in_db.Coinid == coin_in_list.CoinID)
        //             {
        //                 blnCoinExists = true;
        //             }
        //         }

        //         if (!blnCoinExists)
        //         {
        //               CoinNames coinNames = new CoinNames();
        //               coinNames.Coinid = coin_in_list.CoinID;
        //               coinNames.CoinName = coin_in_list.Name;   
        //               _context.CoinNames.Add(coinNames);
        //         }
        //     } 
        //      return await _context.SaveChangesAsync() > 0;
        // }

        // public async Task<bool> AddPriceHistory(List<coins> coinlist)
        // {
        //    decimal BTCPrice = 0;
        //    decimal ETHPrice = 0;

        //    foreach (var coin in coinlist)
        //    {
        //        if (coin.CoinID == 1)
        //        {
        //            BTCPrice = coin.Price;
        //        }
        //        if (coin.CoinID == 1027)
        //        {
        //            ETHPrice = coin.Price;
        //        }
        //    }

        //    foreach (var coin in coinlist)
        //    {
        //        PriceHistory ph = new PriceHistory();
        //        ph.coinid = coin.CoinID;
        //        ph.DateTime = DateTime.Now;
        //        ph.PriceUSD = coin.Price;
        //        ph.priceBTC = Helpers.coinPriceConversions.priceinBTC(BTCPrice, coin.Price);
        //        ph.priceETH = Helpers.coinPriceConversions.priceinEth(ETHPrice, coin.Price);
        //        _context.PriceHistory.Add(ph);
        //    }
        //    return await _context.SaveChangesAsync() > 0;
        // }
    }

}