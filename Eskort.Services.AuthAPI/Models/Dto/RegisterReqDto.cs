namespace Eskort.Services.AuthAPI.Models.Dto
{
    public class RegisterReqDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
