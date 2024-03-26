using Module.Core.Models;
using Module.Shared.DTOs.Character;
using Module.Shared.DTOs.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core.Interfaces
{
    public interface IWeaponService 
    {
        Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddWeaponDTO weaponDTO);
    }
}
