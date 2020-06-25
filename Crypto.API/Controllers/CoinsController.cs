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
        private CoinList _coinlist;


        public CoinsController(DataContext context, ICryptoRepository repo, CoinList coinlist)
        {
              _repo = repo;
        }


        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCoins()
        {
            List<int> coins = new List<int>();
            coins = await _repo.GetCoinNamesList();
            _coinlist = await GetCoinsInterval.GetCoinList_API(coins);      // call to set timer going that runs continuasly 
            return Ok(_coinlist);
        }

    }
}