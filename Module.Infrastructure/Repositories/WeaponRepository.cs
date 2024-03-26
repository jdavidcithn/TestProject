using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Core.Interfaces;
using Module.Core.Models;
using Module.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Infrastructure.Repositories
{
    public class WeaponRepository : IWeaponRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public WeaponRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Character> AddWeapon(Weapon weapon)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == weapon.CharacterId);

            var newWeapon = new Weapon
            {
                Name = weapon.Name,
                Damage = weapon.Damage,
                Character = character
            };

            _context.Weapons.Add(newWeapon);
            await _context.SaveChangesAsync();

            return character;
        }
    }
}
