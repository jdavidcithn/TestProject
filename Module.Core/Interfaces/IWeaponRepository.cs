using Module.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core.Interfaces
{
    public interface IWeaponRepository
    {
        Task<Character> AddWeapon(Weapon weapon);
    }
}
