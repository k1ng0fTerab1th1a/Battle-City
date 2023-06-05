using Battle_City.Fields;
using Battle_City.Internal_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City.Game_Elements.Entities
{
    public class Player : Tank
    {
        public Player(int x, int y, Direction dir, Field field) : base(x, y, dir, field) { }
    }
}
