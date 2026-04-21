using CodeBase.Fields;
using CodeBase.Internal_Code;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static CodeBase.Internal_Code.Globals;

namespace CodeBase.Game_Elements.Entities
{
    public abstract class Entity : Visible
    {

        

        public bool IsDead { get; private set; }
        public Entity(int x, int y, int width, int height, Field field) : base(x, y, width, height)
        {
            field.PlaceOnField(this);
        }

        public virtual void Die(Field field)
        {
            IsDead = true;
            field.Remove(this);
        }
    }
}
