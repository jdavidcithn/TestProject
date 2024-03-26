using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Core.Models;
using Module.Shared.DTOs.Character;
using Module.Shared.DTOs.User;
using Newtonsoft.Json;

namespace DotnetRPG.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GatewayController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> GetCharacters()
        {

            //var token = GetStoredToken();

            var token = _httpContextAccessor.HttpContext!.Session.GetString("AuthToken");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.GetAsync("https://localhost:7235/api/character");

            res.EnsureSuccessStatusCode();

            var content = await res.Content.ReadAsStringAsync();

            var deserializedResponse = JsonConvert.DeserializeObject<ServiceResponse<List<GetCharacterDTO>>>(content);

            return Ok(deserializedResponse);
        }
    }
}
