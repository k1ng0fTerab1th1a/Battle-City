using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battle_City.Internal_Code;

namespace Battle_City.Engines
{
    class InputEngine
    {
        public void ProcessInput(ref Direction curdir, out bool isMoving, out bool shoot)
        {
            isMoving = false;
            shoot = false;
            if (!Console.KeyAvailable)
            {
                return;
            }

            isMoving = true;
            ConsoleKey key = ConsoleKey.A;
            while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Spacebar)
                {
                    shoot = true;
                }
            }
            (curdir, isMoving) = key switch
            {
                ConsoleKey.UpArrow => (Direction.Up, true),
                ConsoleKey.DownArrow => (Direction.Down, true),
                ConsoleKey.LeftArrow => (Direction.Left, true),
                ConsoleKey.RightArrow => (Direction.Right, true),
                _ => (curdir, false)
            };
        }
        public bool EnterIsPressed()
        {
            ConsoleKey key;
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    return true;
                }
            }
            return false;
        }

        public void Set()
        {
            GameEngine.ProcessInput += ProcessInput;
        }
    }
}
