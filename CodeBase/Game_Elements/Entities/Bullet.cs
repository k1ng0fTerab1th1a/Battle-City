using Battle_City.Fields;
using Battle_City.Internal_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Battle_City.Internal_Code.Globals;

namespace Battle_City.Game_Elements.Entities
{
    public class Bullet : MovingEntity
    {
        public readonly Tank Master;
        public readonly Type MasterType;
        public Bullet(int x, int y, Direction dir, Tank master, Field field) : base(x, y, dir, BulletWidth, BulletHeight, field)
        {
            Speed = 1;

            IsMoving = true;

            Master = master;
            MasterType = master.GetType();
        }


        protected override void MoveHelper(Field field, int deltaX, int deltaY)
        {
            int xstart, ystart, xstop, ystop, xstartdestroy, ystartdestroy, xstopdestroy, ystopdestroy;
            ((xstart, ystart, xstop, ystop), (xstartdestroy, ystartdestroy, xstopdestroy, ystopdestroy)) = (deltaX, deltaY) switch
            {
                (0, -1) => ((X, Y - Speed, X + Width, Y), (X - 1, Y - 1, X + 3, Y)),
                (0, 1) => ((X, Y + Height, X + Width, Y + Height + Speed), (X - 1, Y + Height, X + Width + 1, Y + Height + 1)),
                (-1, 0) => ((X - Speed, Y, X, Y + Height), (X - 1, Y - 1, X, Y + Height + 1)),
                (1, 0) => ((X + Width, Y, X + Width + Speed, Y + Height), (X + Width, Y - 1, X + Width + 1, Y + Height + 1)),
                _ => throw new Exception("Wrong X or Y change value in MoveHelper method of bullet")
            };

            if (field.AreaIsAvailableForBullet(xstart, ystart, xstop, ystop))
            {
                X += Speed * deltaX;
                Y += Speed * deltaY;
                NeedsToBeRemoved = false;
            }
            else
            {
                field.Destroy(xstartdestroy, ystartdestroy, xstopdestroy, ystopdestroy);
                Die(field);
            }
        }
        public override void Move(Field field)
        {
            NeedsToBeRemoved = true;
            base.Move(field);
        }
    }
}
