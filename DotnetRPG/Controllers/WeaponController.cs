using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Core.Interfaces;
using Module.Core.Models;
using Module.Shared.DTOs.Character;
using Module.Shared.DTOs.Weapon;

namespace DotnetRPG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> AddWeapon(AddWeaponDTO weapon)
        {
            var res = await _weaponService.AddWeapon(weapon);

            if(!res.Success)
            {
                return BadRequest();
            }

            return Ok(res);
        }
    }
}
