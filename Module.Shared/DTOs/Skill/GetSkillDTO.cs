using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Shared.DTOs.Skill
{
    public class GetSkillDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
    }
}
