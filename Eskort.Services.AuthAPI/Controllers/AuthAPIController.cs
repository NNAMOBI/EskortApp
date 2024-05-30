using Eskort.Services.AuthAPI.Interface;
using Eskort.Services.AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace Eskort.Services.AuthAPI.Controllers
{
    [Route("api/AuthAPI")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        //private readonly Logger _logger;
        private readonly IAuthRepository _authRepository;
        protected ResponseDto response;
        public AuthAPIController(IAuthRepository authRepository)
        {

            //_logger = logger;
            _authRepository = authRepository;
            response = new ResponseDto();

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterReqDto registerReqDto)
        {
            var res = await _authRepository.Register(registerReqDto);
            if(!string.IsNullOrEmpty(res))
            {
                response.ErrorMessage = res;
                response.Success = false;
                return BadRequest(response);

            }
            response.SuccessMessage = "The user has been added successfully";
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginReqDto loginReq)
        {
            var loginResponse = await _authRepository.Login(loginReq);
            if(loginResponse.AppUser == null)
            {
                response.ErrorMessage = "Username or Password is incorrect! Please check your details and try again";
                response.Success = false;
                return BadRequest(response);
            }

            response.Result = loginResponse;
            response.SuccessMessage = "Your login was successful";
            return Ok(response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RoleDto roleDto)
        {
            var assignRoleSuccessful = await _authRepository.AssignRole(roleDto.Email, roleDto.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                response.ErrorMessage = "Error Encountered! Please check your details and try again";
                response.Success = false;
                return BadRequest(response);
            }


            response.SuccessMessage = "Your role was successfully added";
            return Ok(response);
        }
    }
}
