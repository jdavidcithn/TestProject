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

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> GetCharacter(int id)
        {
            var token = _httpContextAccessor.HttpContext!.Session.GetString("AuthToken");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.GetAsync($"https://localhost:7235/api/character/{id}");

            res.EnsureSuccessStatusCode();

            var content = await res.Content.ReadAsStringAsync();

            var dres = JsonConvert.DeserializeObject <ServiceResponse<GetCharacterDTO>>(content);

            return Ok(dres);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> AddCharacter(AddCharacterDTO character)
        {
            var token = _httpContextAccessor.HttpContext!.Session.GetString("AuthToken");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.PostAsJsonAsync("https://localhost:7235/api/character", character);

            res.EnsureSuccessStatusCode();

            var content = await res.Content.ReadAsStringAsync();

            var dres = JsonConvert.DeserializeObject<ServiceResponse<List<GetCharacterDTO>>>(content);

            return Ok(dres);
        }

        [HttpDelete]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Delete(int id)
        {
            var token = _httpContextAccessor.HttpContext!.Session.GetString("AuthToken");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.DeleteAsync($"https://localhost:7235/api/character/{id}");

            res.EnsureSuccessStatusCode();

            var content = await res.Content.ReadAsStringAsync();

            var dres = JsonConvert.DeserializeObject<ServiceResponse<List<GetCharacterDTO>>>(content);

            return Ok(dres);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> Update(UpdateCharacterDTO character)
        {
            var token = _httpContextAccessor.HttpContext!.Session.GetString("AuthToken");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.PutAsJsonAsync("https://localhost:7235/api/character", character);

            res.EnsureSuccessStatusCode();

            var content = await res.Content.ReadAsStringAsync();

            var dres = JsonConvert.DeserializeObject<ServiceResponse<GetCharacterDTO>>(content);

            return dres;
        }
    }
}
