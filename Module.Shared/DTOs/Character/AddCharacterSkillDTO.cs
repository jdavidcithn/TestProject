using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Shared.DTOs.Character
{
    public class AddCharacterSkillDTO
    {
        public int CharacterId { get; set; }
        public int SkillId { get; set; }
    }
}
