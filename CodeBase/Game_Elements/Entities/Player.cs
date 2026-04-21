using CodeBase.Fields;
using CodeBase.Internal_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Game_Elements.Entities
{
    public class Player : Tank
    {
        public Player(int x, int y, Direction dir, Field field) : base(x, y, dir, field) { }
    }
}
