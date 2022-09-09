using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crypto.API.Data;
using Crypto.API.Dtos;
using System.Threading.Tasks;
using AutoMapper;

namespace Crypto.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly ICryptoRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(ICryptoRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }


        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetaiedDto>(user);

            return Ok(userToReturn);
        }

    }


}