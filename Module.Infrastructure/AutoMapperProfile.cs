using AutoMapper;
using Module.Core.Models;
using Module.Shared.DTOs.Character;
using Module.Shared.DTOs.Skill;
using Module.Shared.DTOs.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<UpdateCharacterDTO, Character>();
            CreateMap<UpdateCharacterDTO, GetCharacterDTO>();
            CreateMap<GetCharacterDTO, Character>();
            CreateMap<AddCharacterDTO, Character>();
            CreateMap<Weapon, GetWeaponDTO>();
            CreateMap<GetWeaponDTO, Weapon>();
            CreateMap<Skill, GetSkillDTO>();
        }
    }
}
