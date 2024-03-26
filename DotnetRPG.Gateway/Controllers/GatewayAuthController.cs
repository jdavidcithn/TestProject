using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Core.Models;
using Module.Shared.DTOs.User;
using Newtonsoft.Json;

namespace DotnetRPG.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayAuthController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GatewayAuthController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDTO userLoginDTO)
        {
            try
            {
                var res = await _httpClient.PostAsJsonAsync("https://localhost:7235/api/auth/login",userLoginDTO);

                res.EnsureSuccessStatusCode();

                var content = await res.Content.ReadAsStringAsync();
                var serviceRes = JsonConvert.DeserializeObject<ServiceResponse<string>>(content);
                var token = serviceRes!.Data;
                _httpContextAccessor.HttpContext!.Session.SetString("AuthToken", token!);
                return Ok(serviceRes);
            }
            catch(HttpRequestException ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

    }
}
