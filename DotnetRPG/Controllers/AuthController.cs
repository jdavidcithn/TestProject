using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Core.Interfaces;
using Module.Core.Models;
using Module.Shared.DTOs.User;

namespace DotnetRPG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDTO userRegister)
        {
            var res = await _authRepository.Register(new User { Username = userRegister.Username }, userRegister.Password);
            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDTO userLogin)
        {
            var res = await _authRepository.Login(userLogin.Username, userLogin.Password);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
