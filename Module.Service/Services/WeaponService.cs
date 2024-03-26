using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Core.Interfaces;
using Module.Core.Models;
using Module.Infrastructure.Data;
using Module.Shared.DTOs.Character;
using Module.Shared.DTOs.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Service.Services
{
    public class WeaponService : IWeaponService
    {
        private readonly IWeaponRepository _weaponRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public WeaponService(IWeaponRepository weaponRepository, IMapper mapper, DataContext context)
        {
            _weaponRepository = weaponRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddWeaponDTO weaponDTO)
        {
            var res = new ServiceResponse<GetCharacterDTO>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == weaponDTO.CharacterId);

                if(character is null)
                {
                    res.Success = false;
                    res.Message = "Character not found";
                    return res;
                }

                var newWeapon = new Weapon
                {
                    Name = weaponDTO.Name,
                    Damage = weaponDTO.Damage,
                    Character = character,
                    CharacterId = character.Id
                };

                var updatedCharacter = await _weaponRepository.AddWeapon(newWeapon);

                res.Data = _mapper.Map<GetCharacterDTO>(updatedCharacter);

            }catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }

            return res;
        }
    }
}
