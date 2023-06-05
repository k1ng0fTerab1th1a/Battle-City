using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City.Game_Elements
{
    public abstract class Visible
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public int Width { protected set; get; }
        public int Height { protected set; get; }

        public Visible(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static event EventHandler? DrawVisible;

        public void Draw()
        {
            DrawVisible?.Invoke(this, EventArgs.Empty);
        }
    }
}
