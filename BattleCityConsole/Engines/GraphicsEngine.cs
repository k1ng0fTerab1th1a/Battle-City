using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Battle_City.Internal_Code.Globals;
using Battle_City.Internal_Code;
using Battle_City.Fields;
using Battle_City.Game_Elements;
using Battle_City.Game_Elements.Cells;
using Battle_City.Game_Elements.Entities;

namespace Battle_City.Engines
{
    public class GraphicsEngine
    {
        private readonly Dictionary<Type, (char, ConsoleColor)> symbols = new Dictionary<Type, (char, ConsoleColor)>()
        {
            { typeof(EmptyCell), (' ', ConsoleColor.Black) },
            { typeof(BrickWall), ('B', ConsoleColor.Red) },
            { typeof(IronWall), ('M', ConsoleColor.White) },
            { typeof(Water), ('W', ConsoleColor.Blue) }
        };

        private readonly Dictionary<(Type, Direction), char[,]> MovingVisual = new Dictionary<(Type, Direction), char[,]>()
        {
            { (typeof(Tank), Direction.Up), new char[,]{
                {' ', 'I', 'I', ' ' },
                {'H', 'O', 'O', 'H' },
                {'H', 'O', 'O', 'H' },
                {'H', 'O', 'O', 'H' }
            } },
            {(typeof(Tank), Direction.Down), new char[,]{
                {'H', 'O', 'O', 'H' },
                {'H', 'O', 'O', 'H' },
                {'H', 'O', 'O', 'H' },
                {' ', 'I', 'I', ' ' }
            } },
            {(typeof(Tank), Direction.Right), new char[,] {
                { 'E', 'E', 'E', ' ' },
                { 'O', 'O', 'O', '-' },
                { 'O', 'O', 'O', '-' },
                { 'E', 'E', 'E', ' ' }
            } },
            {(typeof(Tank), Direction.Left), new char[,] {
                { ' ', 'E', 'E', 'E' },
                { '-', 'O', 'O', 'O' },
                { '-', 'O', 'O', 'O' },
                { ' ', 'E', 'E', 'E' }
            } },
            {(typeof(Bullet), Direction.Up), new char[,] {
                {'/', '\\' },
                {'|', '|' }
            } },
            {(typeof(Bullet), Direction.Down), new char[,] {
                {'|', '|' },
                {'\\', '/' }
            } },
            {(typeof(Bullet), Direction.Right), new char[,] {
                {'-', '\\' },
                {'-', '/'}
            } },
            {(typeof(Bullet), Direction.Left), new char[,] {
                {'/', '-' },
                {'\\', '-' }
            } }
        };


        private readonly Dictionary<Type, ConsoleColor> EntityColors = new Dictionary<Type, ConsoleColor>()
        {
            {typeof(Player), ConsoleColor.Yellow },
            {typeof(Enemy), ConsoleColor.Gray },
            {typeof(Base), ConsoleColor.DarkGray },
            {typeof(Bullet), ConsoleColor.White },
        };

        private readonly static char[,] BaseVisual = new char[,]
        {
            {'<', '*', '*', '>' },
            {'<', '*', '*', '>' },
            {'<', '*', '*', '>' },
            {'<', '*', '*', '>' }
        };
        public void Set()
        {
            Console.SetWindowSize(FieldWidth, FieldHeight);
            Console.SetBufferSize(FieldWidth, FieldHeight);
            Console.CursorVisible = false;
            Visible.DrawVisible += DrawInConsole;
            GameEngine.GameOver += ShowGameOver;
            GameEngine.StartMenu += ShowStartMenu;
            GameEngine.Win += ShowWin;
        }

        private void DrawBase(Base b)
        {
            Console.ForegroundColor = EntityColors.GetValueOrDefault(b.GetType());
            for (int i = b.X; i < b.X + b.Width; i++)
            {
                for (int j = b.Y; j < b.Y + b.Height; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(BaseVisual[j - b.Y, i - b.X]);
                }
            }
        }

        private void DrawMoving(MovingEntity me)
        {
            Type type = me is Tank ? typeof(Tank) : me.GetType();
            char[,] charArr = MovingVisual.GetValueOrDefault((type, me.Dir))!;

            Console.ForegroundColor = EntityColors.GetValueOrDefault(me.GetType());
            for (int i = me.X; i < me.X + me.Width; i++)
            {
                for (int j = me.Y; j < me.Y + me.Height; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(charArr[j - me.Y, i - me.X]);
                }
            }
        }
        private void DrawInConsole(object sender, EventArgs e)
        {
            if (sender is Cell cell)
            {
                Console.SetCursorPosition(cell.X, cell.Y);
                Console.ForegroundColor = symbols.GetValueOrDefault(cell.GetType()).Item2;
                Console.Write(symbols.GetValueOrDefault(cell.GetType()).Item1);
            }
            else if (sender is Base b)
            {
                DrawBase(b);
            }
            else if (sender is Field field)
            {
                for (int i = 0; i < field.Ymax; i++)
                {
                    for (int j = 0; j < field.Xmax; j++)
                    {
                        field[j, i].Draw();
                    }
                    if (i == field.Xmax - 1) break;
                    Console.WriteLine();
                }
            }
            else if (sender is MovingEntity me)
            {
                DrawMoving(me);
            }
        }

        public void ShowGameOver(object sender, EventArgs e)
        {
            Show("Game Over", "Press Enter to play again");
        }

        public void ShowStartMenu(object sender, EventArgs e)
        {
            Show("Battle City", "Press Enter to start...");
        }

        public void ShowWin(object sender, EventArgs e)
        {
            Show("You win!", "Press Enter to play again");
        }

        private void Show(string midText, string botText)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((FieldWidth - midText.Length) / 2, 25);
            Console.WriteLine(midText);
            Console.SetCursorPosition((FieldWidth - botText.Length) / 2, 50);
            Console.WriteLine(botText);
            InputEngine inp = new();
            while (!inp.EnterIsPressed()) { }
        }
    }
}
