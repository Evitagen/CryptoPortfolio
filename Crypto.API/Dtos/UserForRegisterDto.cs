using System.ComponentModel.DataAnnotations;

namespace Crypto.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 8, ErrorMessage = "You must specify password between 8 and 300 characters")]
        public string Password { get; set; }
    }
}