using System.ComponentModel.DataAnnotations;

namespace Eskort.Services.AuthAPI.Models.Dto
{
    public class RegisterReqDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
