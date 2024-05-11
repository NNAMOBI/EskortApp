using Eskort.Services.AuthAPI.Data;
using Eskort.Services.AuthAPI.Interface;
using Eskort.Services.AuthAPI.Models;
using Eskort.Services.AuthAPI.Models.Dto;
using Eskort.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace Eskort.Services.AuthAPI.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthRepository(ApplicationDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _context.AppUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if(user != null)
            {
                //check whether the role exist in the db
                if(!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginReqDto loginReqDto)
        {
            var user = _context.AppUsers.FirstOrDefault(u => u.UserName.ToLower() == loginReqDto.Username.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginReqDto.Password);
            if(user == null|| isValid) 
            {
                return new LoginResponseDto()
                {
                    AppUser = null,
                    Token = ""
                };
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
                AppUserDto appUserDto = new AppUserDto()
                {
                    Email = user.Email,
                    ID = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    Name = user.Name,
                };

                LoginResponseDto loginResponseDto = new LoginResponseDto()
                {
                    AppUser = appUserDto,
                    Token = token
                };
            
            return loginResponseDto;
        }

        public async Task<string> Register(RegisterReqDto registerReqDto)
        {
            AppUser user = new()
            {
                UserName = registerReqDto.Email,
                Email = registerReqDto.Email,
                NormalizedEmail = registerReqDto.Email.ToUpper(),
                Name = registerReqDto.Name,
                PhoneNumber = registerReqDto.PhoneNumber

            };
            try
            {
                var result = await _userManager.CreateAsync(user, registerReqDto.Password);
                if (result.Succeeded)
                {
                    var userResponse = _context.AppUsers.First(u => u.UserName == user.Email);
                    AppUserDto userDto = new()
                    {
                        ID = userResponse.Id,
                        Email = userResponse.Email,
                        PhoneNumber = userResponse.PhoneNumber,
                        Name = userResponse.Name

                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }
    }
}
