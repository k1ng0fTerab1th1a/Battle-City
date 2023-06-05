using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City.Game_Elements.Cells
{
    public abstract class Cell : Visible
    {
        public Cell(int x, int y) : base(x, y, 1, 1)
        {

        }

        public bool TanksCanMove { get; init; }
        public bool BulletsCanMove { get; init; }

        public static readonly Type[] cells =
        {
            typeof(EmptyCell),
            typeof(BrickWall),
            typeof(IronWall),
            typeof(Water)
        };

        public virtual bool IsDestroyable { get { return false; } }
    }    
}