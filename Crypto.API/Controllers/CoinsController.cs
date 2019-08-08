using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypto.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.API.Controllers
{
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
        [HttpGet]
        public IActionResult GetCoins()
        {
           var coins = _context.Coins.ToList();

           return Ok(coins);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

    }
}