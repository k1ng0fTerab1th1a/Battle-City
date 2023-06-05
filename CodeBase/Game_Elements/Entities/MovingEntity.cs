using Battle_City.Fields;
using Battle_City.Internal_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City.Game_Elements.Entities
{
    public abstract class MovingEntity : Entity
    {
        public int Speed { get; protected init; }
        public bool IsMoving = false;

        public bool NeedsToBeRemoved = false;
        public Direction Dir { get; set; }

        private readonly static List<MovingEntity> MovingsOnMap = new List<MovingEntity>();
        public MovingEntity(int x, int y, Direction dir, int width, int height, Field field) : base(x, y, width, height, field)
        {
            Dir = dir;
            MovingsOnMap.Add(this);
        }

        public static void ClearList()
        {
            MovingsOnMap.Clear();
        }

        protected abstract void MoveHelper(Field field, int deltaX, int deltaY);
        public virtual void Move(Field field)
        {
            switch (Dir)
            {
                case Direction.Up:
                    MoveHelper(field, 0, -1);
                    break;
                case Direction.Down:
                    MoveHelper(field, 0, 1);
                    break;
                case Direction.Left:
                    MoveHelper(field, -1, 0);
                    break;
                case Direction.Right:
                    MoveHelper(field, 1, 0);
                    break;
            }
        }

        //protected abstract void MoveHelper(Field field, int deltaX, int deltaY);

        public override void Die(Field field)
        {
            base.Die(field);
            NeedsToBeRemoved = true;
        }

        public static void DrawMovings()
        {
            foreach (MovingEntity me in MovingsOnMap)
            {
                me.Draw();
            }
        }

        public static void MoveAll(Field field, bool moveTanks)
        {
            foreach (MovingEntity me in MovingsOnMap)
            {
                if (me.IsMoving && (!(me is Tank) || moveTanks))
                {
                    field.Remove(me);
                    me.Move(field);
                    if (me.NeedsToBeRemoved)
                    {
                        continue;
                    }
                    field.PlaceOnField(me);

                    (int xstart, int ystart, int xstop, int ystop) = me.Dir switch
                    {
                        Direction.Up => (me.X, me.Y + me.Height, me.X + me.Width - 1, me.Y + me.Height + me.Speed - 1),
                        Direction.Down => (me.X, me.Y - me.Speed, me.X + me.Width - 1, me.Y - 1),
                        Direction.Left => (me.X + me.Width, me.Y, me.X + me.Width + me.Speed - 1, me.Y + me.Height - 1),
                        Direction.Right => (me.X - me.Speed, me.Y, me.X - 1, me.Y + me.Height - 1),
                        _ => (0, 0, 0, 0)
                    };
                    field.Redraw(xstart, ystart, xstop, ystop);
                }
            }

            List<MovingEntity> garbage = MovingsOnMap.Where(x => x.NeedsToBeRemoved).ToList();

            foreach (MovingEntity me in garbage)
            {
                field.Redraw(me.X, me.Y, me.X + me.Width - 1, me.Y + me.Height - 1);
                MovingsOnMap.Remove(me);
            }
        }

        public static void ShootAll(Field field)
        {
            List<MovingEntity> enemies = MovingsOnMap.Where(x => x is Enemy).ToList();
            foreach (Enemy enemy in enemies)
            {
                enemy.RandomShoot(field);
            }
        }

        protected static int CountBulletsDeployedBy(Tank tank)
        {
            return MovingsOnMap.Count(ent => ent is Bullet bul && bul.Master == tank);
        }

        public static int CountEnemies()
        {
            return MovingsOnMap.Count(x => x is Enemy);
        }
    }
}
