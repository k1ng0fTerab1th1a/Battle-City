using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City.Internal_Code
{
    public enum Direction { Left, Right, Up, Down }
    public static class Globals
    {
        public const int FieldWidth = 52;
        public const int FieldHeight = 52;

        public const int TankWidth = 4;
        public const int TankHeight = 4;

        public const int BulletWidth = 2;
        public const int BulletHeight = 2;

        public const int BaseX = 24;
        public const int BaseY = 48;

        public const int FrameMs = 40;
    }
}