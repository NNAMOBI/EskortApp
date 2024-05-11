using Eskort.Services.AuthAPI.Models.Dto;

namespace Eskort.Services.AuthAPI.Interface
{
    public interface IAuthRepository
    {
        Task<string> Register(RegisterReqDto registerReqDto);
        Task<LoginResponseDto> Login(LoginReqDto loginReqDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
