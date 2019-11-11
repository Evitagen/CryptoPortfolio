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

namespace Crypto.API.Controllers
{
    [Authorize] // authorise so have to be logged in
    [Route("api/[controller]")]
    [ApiController]
    public class CoinsController : ControllerBase
    {
        private readonly DataContext _context;

        public CoinsController(DataContext context)
        {
            _context = context;
        }


          // GET api/values
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCoins()
        {
         CoinList coinmcap = new CoinList();

         //coinmcap = await GetCoinsInterval.GetCoinList();
         coinmcap = await GetCoinsInterval.GetCoinList_API();

         coinmcap.viewCoins();
  
         return Ok(coinmcap);
        }

        //      // GET api/values
        [AllowAnonymous]
        [HttpGet("Mcap/")]
        public async Task<IActionResult> GetMarketCapString()
        {
         CoinList coinmcap = new CoinList();

         coinmcap = await GetCoinsInterval.GetCoinList();
  
         return Ok(coinmcap);
        }


    }
}