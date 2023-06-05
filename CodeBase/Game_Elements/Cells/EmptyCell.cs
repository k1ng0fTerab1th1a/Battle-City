using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City.Game_Elements.Cells
{
    public class EmptyCell : Cell
    {
        public EmptyCell(int x, int y) : base(x, y)
        {
            TanksCanMove = true;
            BulletsCanMove = true;
        }
    }
}
