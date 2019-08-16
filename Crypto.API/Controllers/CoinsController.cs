using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypto.API.Data;
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
           var coinsdb = await _context.Coins.ToListAsync();


          CoinList coinmcap = new CoinList();

          // this must not be done for each client call but on a seperate thread and just value read
          coinmcap.loadcoinMCapData();
          





           return Ok(coinmcap);
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Coins.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(value);
        }
        
    }
}