using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Core.Interfaces;
using Module.Core.Models;
using Module.Shared.DTOs.Character;
using Newtonsoft.Json;

namespace DotnetRPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> GetCharacters()
        {
            var characters = await _characterService.GetAllCharacters();

            return Content(JsonConvert.SerializeObject(characters), "application/json");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> GetCharacterById(int id) 
        {
            var res = await _characterService.GetCharacterById(id);

            if(res.Success)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> AddCharacter (AddCharacterDTO character)
        {
            var res = await _characterService.AddCharacter(character);

            if(res.Success)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> DeleteCharacter (int id)
        {
            var res = await _characterService.DeleteCharacter(id);

            if(res.Success)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }

        [HttpPut]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> UpdateCharacter(UpdateCharacterDTO character)
        {
            var res = await _characterService.UpdateCharacter(character);

            if( res.Success)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }
    }
}
