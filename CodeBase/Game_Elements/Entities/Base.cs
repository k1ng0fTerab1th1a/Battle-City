using CodeBase.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Game_Elements.Entities
{
    public class Base : Entity
    {
        public Base(int x, int y, Field field) : base(x, y, 4, 4, field)
        {

        }
    }
}
