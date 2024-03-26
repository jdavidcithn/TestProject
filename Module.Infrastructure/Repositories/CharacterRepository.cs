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
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CharacterRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Character>> AddCharacter(Character character)
        {
            var newCharacter = character;
            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();
            return await _context.Characters.ToListAsync();
        }

        public async Task<List<Character>> DeleteCharacter(int id)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

            if(character == null)
            {
                throw new Exception($"Character with id {id} not found");
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return await _context.Characters.ToListAsync();
        }

        public async Task<List<Character>> GetAllCharacters()
        {
            var characters = await  _context.Characters.ToListAsync();
            return characters;
        }

        public async Task<Character> GetCharactersById(int id)
        {
            var character =  await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

            return character;
        }

        public async Task<Character> UpdateCharacter(Character character)
        {
            var existingCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == character.Id);

            //existingCharacter!.Name = character.Name;
            //existingCharacter.HitPoints = character.HitPoints;
            //existingCharacter.Strength = character.Strength;
            //existingCharacter.Defense = character.Defense;
            //existingCharacter.Intelligence = character.Intelligence;
            //existingCharacter.Class = character.Class;

            _mapper.Map(character, existingCharacter);

            _context.Entry(existingCharacter).CurrentValues.SetValues(character);

            await _context.SaveChangesAsync();

            return character;
        }
    }
}
