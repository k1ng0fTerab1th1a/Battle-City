using Battle_City.Game_Elements.Cells;
using static BattleCityGUI.Internal_Code.GUIGlobals;
using static Battle_City.Internal_Code.Globals;
using BattleCityGUI.Properties;
using Battle_City.Game_Elements;
using Battle_City.Fields;
using BattleCityGUI.Forms;
using Battle_City.Game_Elements.Entities;
using Battle_City.Internal_Code;
using System.Numerics;

namespace BattleCityGUI
{
    public class GraphicsEngine
    {
        private static readonly Dictionary<Type, Image> CellImages = new()
        {
            {typeof(BrickWall), Resources.brickWall },
            {typeof(IronWall), Resources.ironWall },
            {typeof(Water), Resources.water },
        };

        private static readonly Dictionary<(Type, Direction), (Image, Action<Image>)> MovingsVisual = new()
        {
            { (typeof(Player), Direction.Up), (Resources.player, (Image x) => x.RotateFlip(RotateFlipType.RotateNoneFlipNone)) },
            { (typeof(Player), Direction.Right), (Resources.player, (Image x) => x.RotateFlip(RotateFlipType.Rotate90FlipNone)) },
            { (typeof(Player), Direction.Down), (Resources.player, (Image x) => x.RotateFlip(RotateFlipType.Rotate180FlipNone)) },
            { (typeof(Player), Direction.Left), (Resources.player, (Image x) => x.RotateFlip(RotateFlipType.Rotate270FlipNone)) },

            { (typeof(Enemy), Direction.Up), (Resources.enemy, (Image x) => x.RotateFlip(RotateFlipType.RotateNoneFlipNone)) },
            { (typeof(Enemy), Direction.Right), (Resources.enemy, (Image x) => x.RotateFlip(RotateFlipType.Rotate90FlipNone)) },
            { (typeof(Enemy), Direction.Down), (Resources.enemy, (Image x) => x.RotateFlip(RotateFlipType.Rotate180FlipNone)) },
            { (typeof(Enemy), Direction.Left), (Resources.enemy, (Image x) => x.RotateFlip(RotateFlipType.Rotate270FlipNone)) },

            { (typeof(Bullet), Direction.Up), (Resources.bullet, (Image x) => x.RotateFlip(RotateFlipType.RotateNoneFlipNone)) },
            { (typeof(Bullet), Direction.Right), (Resources.bullet, (Image x) => x.RotateFlip(RotateFlipType.Rotate90FlipNone)) },
            { (typeof(Bullet), Direction.Down), (Resources.bullet, (Image x) => x.RotateFlip(RotateFlipType.Rotate180FlipNone)) },
            { (typeof(Bullet), Direction.Left), (Resources.bullet, (Image x) => x.RotateFlip(RotateFlipType.Rotate270FlipNone)) }
        };



        public static GameForm? gameForm;

        private Dictionary<(int, int), PictureBox> CellPictureBoxes = new();
        private Dictionary<MovingEntity, PictureBox> MovingsPictureBoxes = new();
        public void Set()
        {
            GameForm.ResetForm += Reset;
            Visible.DrawVisible += DrawGUI;
            MovingEntity.EntityDied += DeletePictureBox;
            foreach (KeyValuePair<(Type, Direction), (Image, Action<Image>)> kvp in MovingsVisual)
            {
                kvp.Value.Item2(kvp.Value.Item1);
            }
        }

        public void Reset(object? sender, EventArgs e)
        {
            CellPictureBoxes.Clear();
            MovingsPictureBoxes.Clear();
        }
        public void DrawGUI(object? sender, EventArgs e)
        {
            if (sender is Cell cell)
            {
                DrawCell(cell);
            }
            else if (sender is Field field)
            {
                for (int i = 0; i < field.Ymax; i++)
                {
                    for (int j = 0; j < field.Xmax; j++)
                    {
                        field[j, i].Draw();
                    }
                }
            }
            else if (sender is Base @base)
            {
                DrawBase(@base);
            }
            else if (sender is MovingEntity moving)
            {
                DrawMoving(moving);
            }
        }

        private void DrawCell(Cell cell)
        {
            if (!CellPictureBoxes.ContainsKey((cell.X, cell.Y)) && cell is not EmptyCell)
            {
                PictureBox pb = new()
                {
                    Location = new Point(cell.X * CellWidth, cell.Y * CellHeight),
                    Size = new Size(CellWidth, CellHeight),
                    Image = CellImages.GetValueOrDefault(cell.GetType()),
                    BackColor = Color.Transparent
                };
                CellPictureBoxes.Add((cell.X, cell.Y), pb);
                pb.Parent = gameForm;
                gameForm?.Controls.Add(pb);
            }
            else if (cell is EmptyCell && CellPictureBoxes.ContainsKey((cell.X, cell.Y)))
            {
                CellPictureBoxes.GetValueOrDefault((cell.X, cell.Y))?.Dispose();
            }
        }

        private void DrawBase(Base @base)
        {
            PictureBox pb = new()
            {
                Location = new Point(BaseX * CellWidth, BaseY * CellHeight),
                Size = new Size(@base.Width * CellWidth, @base.Height * CellHeight),
                Image = Resources._base,
                BackColor = Color.Transparent
            };
            pb.Parent = gameForm;
            gameForm?.Controls.Add(pb);
        }

        private void DrawMoving(MovingEntity moving)
        {
            if (!MovingsPictureBoxes.ContainsKey(moving))
            {
                PictureBox pb = new()
                {
                    Location = new Point(moving.X * CellWidth, moving.Y * CellHeight),
                    Size = new Size(TankWidth * CellWidth, TankHeight * CellHeight),
                    //Image = Resources.player,
                    Image = MovingsVisual.GetValueOrDefault((moving.GetType(), moving.Dir)).Item1,
                    BackColor = Color.Empty,

                };
                
                MovingsPictureBoxes.Add(moving, pb);
                pb.Parent = gameForm;
                gameForm?.Controls.Add(pb);
            }
            else
            {
                PictureBox pb = MovingsPictureBoxes.GetValueOrDefault(moving)!;
                pb.Location = new Point(moving.X * CellWidth, moving.Y * CellHeight);
                pb.Image = MovingsVisual.GetValueOrDefault((moving.GetType(), moving.Dir)).Item1;
            }
        }

        private void DeletePictureBox(object? sender, EventArgs e)
        {
            var waste = MovingsPictureBoxes.GetValueOrDefault(sender as MovingEntity);
            gameForm?.Controls.Remove(waste);
            MovingsPictureBoxes.Remove(sender as MovingEntity);
            waste?.Dispose();
        }
    }
}
