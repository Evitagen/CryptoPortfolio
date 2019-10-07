using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crypto.API.Data;
using Crypto.API.Dtos;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Crypto.API.Models;

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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPortfolio(int id)
        {

             var Portfolio = await _repo.GetPortfolio(id);

             //var PortfolioToReturn = _mapper.Map<PortFoliosForDetailedDto>(Portfolio);

             // must check only users portfolios are returned not other users

             return Ok(Portfolio);

        }

         [HttpPost("Add/{name}/{id}")]
         public async Task<IActionResult> Add(string name, int id)
         {
            
            // must check the id passing in is match for id from jwt token

            User user = await _repo.GetUser(id);

            if (await _repo.AddPortfolio(name, user))
                return Ok();
            
             return BadRequest("Failed to add Portfolio");
         }


         [HttpPost("AddCoin/{name}/{portFolioid}")]
          public async Task<IActionResult> AddCoin(string name, int portFolioid)
         { 
            // must check the id passing in is match for id from jwt token
            Portfolio portfolio = await _repo.GetPortfolio(portFolioid);

            if (await _repo.AddCoinToPortfolio(name, portfolio))
               return Ok();
  
            return BadRequest("Failed to add Coin");
         }

    }
}