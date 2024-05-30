using System.ComponentModel.DataAnnotations;

namespace Eskort.Services.AuthAPI.Models.Dto
{
    public class LoginReqDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
