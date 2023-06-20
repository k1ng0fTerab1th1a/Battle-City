using Battle_City.Fields;
using Battle_City.Game_Elements.Cells;
using CodeBase;
using CodeBase.Properties;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using static Battle_City.Internal_Code.Globals;

namespace Battle_City.Internal_Code
{
    public static class Build
    {
        private readonly static Dictionary<char, Type> types = new()
        {
            { ' ', typeof(EmptyCell) },
            { 'B' , typeof(BrickWall) },
            { 'M' , typeof(IronWall) },
            { 'W' , typeof(Water) }
        };
        public static void Block(Field field, int x, int y, Type cellType)
        {
            for (int i = x; i < x + 4; i++)
            {
                for (int j = y; j < y + 4; j++)
                {
                    field[i, j] = (Cell)Activator.CreateInstance(cellType, i, j)!;
                }
            }
        }
        public static void Block(Field field, int x, int y, params Type[] cellTypes)
        {
            int[,] block =
            {
                { 0, 0, 1, 1 },
                { 0, 0, 1, 1 },
                { 2, 2, 3, 3 },
                { 2, 2, 3, 3 },
            };

            for (int i = x; i < x + 4; i++)
            {
                for (int j = y; j < y + 4; j++)
                {
                    field[i, j] = (Cell)Activator.CreateInstance(cellTypes[block[j - y, i - x]], i, j)!;
                }
            }
        }
        public static void Fill(Field field, int xstart, int ystart, int xstop, int ystop, Type cellType)
        {
            for (int i = xstart; i < xstop; i++)
            {
                for (int j = ystart; j < ystop; j++)
                {
                    field[i, j] = (Cell)Activator.CreateInstance(cellType, i, j)!;
                }
            }
        }
        public static void Fill(Field field, Type cellType)
        {
            for (int i = 0; i < field.Xmax; i++)
            {
                for (int j = 0; j < field.Ymax; j++)
                {
                    field[i, j] = (Cell)Activator.CreateInstance(cellType, i, j)!;
                }
            }
        }
        public static void Base(Field field)
        {
            Block(field, BaseX - 4, BaseY,
                typeof(EmptyCell), typeof(BrickWall),
                typeof(EmptyCell), typeof(BrickWall));
            Block(field, BaseX - 4, BaseY - 4,
                typeof(EmptyCell), typeof(EmptyCell),
                typeof(EmptyCell), typeof(BrickWall));
            Block(field, BaseX, BaseY - 4,
                typeof(EmptyCell), typeof(EmptyCell),
                typeof(BrickWall), typeof(BrickWall));
            Block(field, BaseX + 4, BaseY - 4,
                typeof(EmptyCell), typeof(EmptyCell),
                typeof(BrickWall), typeof(EmptyCell));
            Block(field, BaseX + 4, BaseY,
                typeof(BrickWall), typeof(EmptyCell),
                typeof(BrickWall), typeof(EmptyCell));
            Block(field, BaseX, BaseY, typeof(EmptyCell));
            Block(field, BaseX - 8, BaseY, typeof(EmptyCell));
        }

        private static void DecodeLevel(Field field, string map)
        {
            //fhieght.fwidth.basex.basey.playerx.playery.enemy1spawnx.enemy1spawny.enemy2spawnx.enemy2spawny.enemy3spawnx.enemy3spawny - .....
            string[] lines = map.Split("\n");
            for (int i = 0; i < field.Height; i++)
            {
                for (int j = 0; j < field.Width; j++)
                {
                    field[i, j] = (Cell)Activator.CreateInstance(types.GetValueOrDefault(lines[j][i])!, i, j)!;
                }
            }
        }
        public static void Stage1(Field field)
        {
            /*
            Fill(field, typeof(EmptyCell));
            Fill(field, 4, 4, 8, 24, typeof(BrickWall));
            Fill(field, 12, 4, 16, 24, typeof(BrickWall));

            Fill(field, 20, 4, 24, 20, typeof(BrickWall));
            Block(field, 24, 14, typeof(IronWall));
            Fill(field, 28, 4, 32, 20, typeof(BrickWall));

            Fill(field, 36, 4, 40, 24, typeof(BrickWall));
            Fill(field, 44, 4, 48, 24, typeof(BrickWall));

            Block(field, 20, 24, typeof(BrickWall));
            Block(field, 28, 24, typeof(BrickWall));

            Block(field, 0, 28,
                typeof(BrickWall), typeof(BrickWall),
                typeof(IronWall), typeof(IronWall));
            Fill(field, 8, 28, 16, 32, typeof(BrickWall));
            Fill(field, 36, 28, 44, 32, typeof(BrickWall));
            Block(field, 48, 28,
                typeof(BrickWall), typeof(BrickWall),
                typeof(IronWall), typeof(IronWall));

            Fill(field, 4, 36, 8, 48, typeof(BrickWall));
            Fill(field, 12, 36, 16, 48, typeof(BrickWall));

            Fill(field, 20, 32, 24, 42, typeof(BrickWall));
            Block(field, 24, 34, typeof(BrickWall));
            Fill(field, 28, 32, 32, 42, typeof(BrickWall));

            Fill(field, 36, 36, 40, 48, typeof(BrickWall));
            Fill(field, 44, 36, 48, 48, typeof(BrickWall));

            Base(field);
            */
            DecodeLevel(field, CodeBase.Properties.Resources.lvl1);
        }
    }
}
