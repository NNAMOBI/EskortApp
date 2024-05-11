using Microsoft.AspNetCore.Identity;

namespace Eskort.Services.AuthAPI.Models
{
    public class AppUser:  IdentityUser
    {
        public string Name { get; set; }
    }
}
