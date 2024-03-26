using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Core.Interfaces;
using Module.Core.Models;
using Module.Infrastructure.Data;
using Module.Shared.DTOs.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Service.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(ICharacterRepository characterRepository, IMapper mapper, DataContext context)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO character)
        {
            var serviceRes = new ServiceResponse<List<GetCharacterDTO>>();
            var newCharacter = _mapper.Map<Character>(character);
            await _characterRepository.AddCharacter(newCharacter);
            serviceRes.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
            return serviceRes;
        }

        public Task<ServiceResponse<GetCharacterDTO>> AddCharacterSkill(AddCharacterSkillDTO characterSkill)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var serviceRes = new ServiceResponse<List<GetCharacterDTO>> ();

            try
            {
                await _characterRepository!.DeleteCharacter(id);
                serviceRes.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDTO> (c)).ToListAsync();

            }catch (Exception ex)
            {
                serviceRes.Success = false;
                serviceRes.Message = ex.Message;
            }

            return serviceRes;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            var serviceResponse =  new ServiceResponse<List<GetCharacterDTO>>();
            var characters = await _characterRepository.GetAllCharacters();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            var character = await _characterRepository.GetCharactersById(id);

            if(character is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Character with id {id} was not found";
                return serviceResponse;
            }

            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {

            var serviceRes = new ServiceResponse<GetCharacterDTO>();

            try
            {
                var updateCharacter = _mapper.Map<Character>(updatedCharacter);

                var updateddCharacter = await _characterRepository.UpdateCharacter(updateCharacter);

                var character = _mapper.Map<GetCharacterDTO>(updatedCharacter);

                serviceRes.Data = character;
            }
            catch (Exception ex)
            {
                serviceRes.Success = false;
                serviceRes.Message = ex.Message;
            }

            return serviceRes;
        }
    }
}
