using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crypto.API.Data;
using Crypto.API.Dtos;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
    }
}