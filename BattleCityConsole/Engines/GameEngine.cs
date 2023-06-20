using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battle_City.Internal_Code;
using static Battle_City.Internal_Code.Globals;
using Battle_City.Fields;
using Battle_City.Game_Elements.Entities;
using System.ComponentModel;

namespace Battle_City.Engines
{
    public delegate void ProcessInput(ref Direction curdir, out bool isMoving, out bool shoot);
    public class GameEngine
    {
        public static event EventHandler? StartMenu;
        public static event EventHandler? GameOver;
        public static event EventHandler? Win;

        public static ProcessInput? ProcessInput;
        private enum GameState { Win, Lose, OnGoing }

        
        public void Run()
        {
            StartMenuMethod();

            MovingEntity.ClearList();
            Field field = new Field(FieldWidth, FieldHeight);
            //field.ClearField();
            Base @base = new Base(BaseX, BaseY, field);
            Build.Stage1(field);
            Player player = new Player(16, 48, Direction.Up, field);

            
            field.Draw();
            @base.Draw();
            MovingEntity.DrawMovings();
            GameState gameState = GameState.OnGoing;
            int _ticks = 0;
            int enemiesLeft = 20;
            Direction dir;
            bool shoot;
            do
            {
                dir = player.Dir;
                ProcessInputMethod(ref dir, out player.IsMoving, out shoot);
                player.Dir = dir;
                if (shoot)
                {
                    player.Shoot(field);
                }
                MovingEntity.MoveAll(field, _ticks % 2 == 0);
                MovingEntity.ShootAll(field);
                if (player.IsDead || @base.IsDead)
                {
                    gameState = GameState.Lose;
                }

                if (enemiesLeft > 0)
                {
                    if (_ticks % 400 == 101)
                    {
                        new Enemy(0, 0, Direction.Down, field);
                        enemiesLeft--;
                    }
                    else if (_ticks % 400 == 201)
                    {
                        new Enemy(24, 0, Direction.Down, field);
                        enemiesLeft--;
                    }
                    else if (_ticks % 400 == 301)
                    {
                        new Enemy(48, 0, Direction.Down, field);
                        enemiesLeft--;
                    }
                }
                else if (MovingEntity.CountEnemies() == 0)
                {
                    gameState = GameState.Win;
                }

                

                MovingEntity.DrawMovings();
                Thread.Sleep(FrameMs);
                _ticks++;
            } while (gameState == GameState.OnGoing);

            if (gameState == GameState.Lose)
            {
                GameOverMethod();
            }
            else if (gameState == GameState.Win)
            {
                WinMethod();
            }
        }

        public void GameOverMethod()
        {
            GameOver?.Invoke(this, EventArgs.Empty);
        }

        public void StartMenuMethod()
        {
            StartMenu?.Invoke(this, EventArgs.Empty);
        }

        public void WinMethod()
        {
            Win?.Invoke(this, EventArgs.Empty);
        }

        public void ProcessInputMethod(ref Direction curdir, out bool isMoving, out bool shoot)
        {
            bool _ismoving = false;
            bool _shoot = false;
            ProcessInput?.Invoke(ref curdir, out _ismoving, out _shoot);
            isMoving = _ismoving;
            shoot = _shoot;
        }
    }
}
