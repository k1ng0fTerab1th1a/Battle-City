using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City.Game_Elements.Cells
{
    public class BrickWall : Wall
    {
        public BrickWall(int x, int y) : base(x, y)
        {

        }
        public override bool IsDestroyable { get { return true; } }
    }
}
