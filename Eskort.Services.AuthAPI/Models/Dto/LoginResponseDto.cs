namespace Eskort.Services.AuthAPI.Models.Dto
{
    public class LoginResponseDto
    {
        public AppUserDto AppUser { get; set; }
        public string Token { get; set; }   
    }
}
