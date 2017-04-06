using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashFront.Models.Characters
{
    public abstract class BaseCharacter
    {
        public String Name { get; set; }
        public List<Move> Moveset { get; set; }
    }
}
