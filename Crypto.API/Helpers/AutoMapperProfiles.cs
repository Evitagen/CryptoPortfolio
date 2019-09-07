using AutoMapper;
using Crypto.API.Models;
using Crypto.API.Dtos;

namespace Crypto.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
           CreateMap<User, CoinsForUsersDto>(); 
           CreateMap<CoinsHodle, CoinsForDetailedDto>();
        }
    }
}