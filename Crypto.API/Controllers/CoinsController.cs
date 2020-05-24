using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypto.API.Data;
using Crypto.API.Helpers;
using Crypto.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Crypto.API.Controllers
{
    [Authorize] // authorise so have to be logged in
    [Route("api/[controller]")]
    [ApiController]
    public class CoinsController : ControllerBase
    {
        private DataContext _context;
        private ICryptoRepository _repo;
        // private GetCoinsInterval _coinsInterval;
        private CoinList _coinlist;



        // private IServiceCollection _services;

        public CoinsController(DataContext context, ICryptoRepository repo, CoinList coinlist)
        {
              _repo = repo;
            // // _context = context;
            // _coinsInterval = coinsInterval;
           //  _coinlist = coinlist;
            //  _services = services;
        }


        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCoins()
        {
        //   CoinList coinmcap = new CoinList(_repo);

         List<int> coins = new List<int>();
         coins = await _repo.GetCoinNamesList();
        
        
        //   coinmcap = await GetCoinsInterval.GetCoinList();
         _coinlist = await GetCoinsInterval.GetCoinList_API(coins);

  
         return Ok(_coinlist);
        }

        //      // GET api/values
        // [AllowAnonymous]
        // [HttpGet("Mcap/")]
        // public async Task<IActionResult> GetMarketCapString()
        // {
        //  CoinList coinmcap = new CoinList();

        //  coinmcap = await GetCoinsInterval.GetCoinList();
  
        //  return Ok(coinmcap);
        // }


    }
}