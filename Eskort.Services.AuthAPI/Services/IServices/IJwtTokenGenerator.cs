using Eskort.Services.AuthAPI.Models;

namespace Eskort.Services.AuthAPI.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser appUser);
    }
}
