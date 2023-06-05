using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City.Game_Elements.Cells
{
    public abstract class Wall : Cell
    {
        protected Wall(int x, int y) : base(x, y)
        {
            TanksCanMove = false;
            BulletsCanMove = false;
        }
    }
}
