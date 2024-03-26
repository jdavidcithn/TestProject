using Module.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core.Interfaces
{
    public interface ICharacterRepository
    {
        Task<Character> GetCharactersById(int id);
        Task<List<Character>> GetAllCharacters();
        Task<List<Character>> AddCharacter (Character character);
        Task<Character> UpdateCharacter (Character character);
        Task<List<Character>> DeleteCharacter (int id);
    }
}
