﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Shared.DTOs.Weapon
{
    public class GetWeaponDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
    }
}
