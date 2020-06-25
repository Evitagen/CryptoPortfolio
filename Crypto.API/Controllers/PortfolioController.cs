using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crypto.API.Data;
using Crypto.API.Dtos;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Crypto.API.Models;
using System;
using System.Collections.Generic;

namespace Crypto.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICryptoRepository _repo;

        public PortfolioController(ICryptoRepository repo, DataContext context, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }



        //////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////// GET Portfolio
        //////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPortfolio(int id)
        {

             var Portfolio = await _repo.GetPortfolio(id);

             //var PortfolioToReturn = _mapper.Map<PortFoliosForDetailedDto>(Portfolio);

             // go through all coins in portfolio and update the coin holdings from transaction
             

             //
             // must check only users portfolios are returned not other users
             //

             return Ok(Portfolio);

        }

        //////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////// GET ALL PortfolioS
         //////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("All/{id}")]
        public async Task<IActionResult> GetAllPortfolio(int id)
        {

            User user = await _repo.GetUser(id);
            var Portfolios = await _repo.GetAllPortfolios(user);

             // instead of doing this could create a DTO?
            for (int i = 0; i < Portfolios.Count; i++)
            {
                Portfolios[i].User = null;  // removes all user details containing hash etc
            }

             //
             // must check only users portfolios are returned not other users
             //

             return Ok(Portfolios);
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////// GET All TRANSACTIONS
         //////////////////////////////////////////////////////////////////////////////////////////


        [HttpGet("AllTran/{ids}")]
        public async Task<IActionResult> GetAllTran(string ids)
        {
            try 
            {
              var Transactions = await _repo.GetTransactions(ids);
              return Ok(Transactions);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                return BadRequest("Failed to get transactions");
            }
 
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////// ADD PORTFOLIO
        //////////////////////////////////////////////////////////////////////////////////////////

         [HttpPost("Add/{name}/{id}")]
         public async Task<IActionResult> Add(string name, int id)
         {
            
            // must check the id passing in is match for id from jwt token

            User user = await _repo.GetUser(id);

            if (await _repo.AddPortfolio(name, user))
                return Ok();
            
             return BadRequest("Failed to add Portfolio");
         }


        //////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////// ADD COIN TO PORTFOLIO
         //////////////////////////////////////////////////////////////////////////////////////////

         [HttpPost("AddCoin/{coinidno}/{portFolioid}")]
          public async Task<IActionResult> AddCoin(int coinid, int coinidno, int portFolioid)            // need to add coinid aswell
         { 
             System.Console.WriteLine("add coin");
            // must check the id passing in is match for id from jwt token as somone could hack other users
            Portfolio portfolio = await _repo.GetPortfolio(portFolioid);

            if (await _repo.AddCoinToPortfolio(coinid, coinidno, portfolio))
               return Ok();
  
            return BadRequest("Failed to add Coin");
         }





        //////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////// ADD TRANSACTION
        //////////////////////////////////////////////////////////////////////////////////////////

         [HttpPost("AddTransaction/{coinname}/{coinhodleid}/{quantity}/{fee}/{datatime}/{priceWhenBoughtSold}/{coinid}")]
         public async Task<IActionResult> AddTransaction(string coinname, int coinhodleid, decimal quantity, decimal fee, string datatime, decimal priceWhenBoughtSold, int coinid)
         {

            try 
            {
                CoinsHodle coinsHodle = await _repo.GetCoinsHodle(coinhodleid);

                //Transactions transactions = await _repo.GetTransactions(coinsHodle.Id);



                if (await _repo.AddTransaction(coinsHodle, quantity, datatime, fee, priceWhenBoughtSold, coinid))
                {
                   // var total = await _repo.Get_Total_Coin_In_Portfolio(coinsHodle, coinid);                     // replace 1 with coinid


                    // if (await _repo.UpdateCoinHodleAmount(coinsHodle))              // maybe better way of doing this

                    //return Ok(total);
                    return Ok();
                }
               
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }


               
            return BadRequest("Failed to add Transaction");
             
         }

    }
}