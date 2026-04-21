using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Game_Elements.Cells
{
    public class Water : Cell
    {
        public Water(int x, int y) : base(x, y)
        {
            TanksCanMove = false;
            BulletsCanMove = true;
        }
    }
}
