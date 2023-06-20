using static BattleCityGUI.Internal_Code.GUIGlobals;
using static Battle_City.Internal_Code.Globals;
using BattleCityGUI.Properties;
using BattleCityGUI.Forms;
using Battle_City.Fields;
using Battle_City.Game_Elements.Entities;
using Battle_City.Internal_Code;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Numerics;

namespace BattleCityGUI
{
    public partial class GameForm : FormBase
    {
        public delegate void ProcessInput(ref Direction curdir, ref bool isMoving, ref bool shoot);
        public static event EventHandler? ResetForm;
        private enum GameState { Win, Lose, OnGoing }
        public GameForm() : base()
        {
            //this.BackColor = Color.Yellow;
            this.KeyDown += OnKeyDown;
            GraphicsEngine.gameForm = this;
            this.BackColor = Color.Black;
        }

        private Field? field;
        private Base? @base;
        private Player? player;
        private GameState gameState;
        private Direction dir;
        private bool shoot;
        private int enemiesLeft;
        private int _ticks;
        private System.Windows.Forms.Timer timer = new();
        public void RunGame()
        {
            ResetForm?.Invoke(this, EventArgs.Empty);
            MovingEntity.ClearList();
            field = new Field(FieldWidth, FieldHeight);
            //field.ClearField();
            @base = new Base(BaseX, BaseY, field);
            Build.Stage1(field);
            player = new Player(16, 48, Direction.Up, field);


            field.Draw();
            @base.Draw();
            MovingEntity.DrawMovings();
            gameState = GameState.OnGoing;
            _ticks = 0;
            enemiesLeft = 20;
            shoot = false;

            timer.Tick += UpdateGame;
            timer.Interval = FrameMs;
            timer.Start();
        }

        private Direction _direction;
        private bool _isMoving;
        private bool _shoot;
        private int _currentTick;
        private int _lastTick;
        private void ProcessInputMethod(ref Direction curdir, ref bool isMoving, ref bool shoot, int ticks)
        {
            curdir = _direction;
            isMoving = _isMoving;
            shoot = _shoot;
            _currentTick = ticks;
            _isMoving = false;
            _shoot = false;
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (_lastTick != _currentTick)
            {
                _isMoving = false;
                _shoot = false;
            }

            if (e.KeyCode == Keys.Space)
            {
                _shoot = true;
            }
            switch (e.KeyCode)
            {
                case Keys.Up:
                    _isMoving = true;
                    _direction = Direction.Up;
                    break;
                case Keys.Down:
                    _isMoving = true;
                    _direction = Direction.Down;
                    break;
                case Keys.Left:
                    _isMoving = true;
                    _direction = Direction.Left;
                    break;
                case Keys.Right:
                    _isMoving = true;
                    _direction = Direction.Right;
                    break;
                default:
                    break;
            }

            _lastTick = _currentTick;
        }

        private void UpdateGame(object? sender, EventArgs e)
        {
            dir = player!.Dir;
            ProcessInputMethod(ref dir, ref player.IsMoving, ref shoot, _ticks);
            player.Dir = dir;
            if (shoot)
            {
                player.Shoot(field!);
            }
            MovingEntity.MoveAll(field!, _ticks % 2 == 0);
            MovingEntity.ShootAll(field!);
            if (player.IsDead || @base!.IsDead)
            {
                gameState = GameState.Lose;
            }

            if (enemiesLeft > 0)
            {
                if (_ticks % 400 == 101)
                {
                    new Enemy(0, 0, Direction.Down, field!);
                    enemiesLeft--;
                }
                else if (_ticks % 400 == 201)
                {
                    new Enemy(24, 0, Direction.Down, field!);
                    enemiesLeft--;
                }
                else if (_ticks % 400 == 301)
                {
                    new Enemy(48, 0, Direction.Down, field!);
                    enemiesLeft--;
                }
            }
            else if (MovingEntity.CountEnemies() == 0)
            {
                gameState = GameState.Win;
            }



            MovingEntity.DrawMovings();
            _ticks++;

            if (gameState == GameState.Lose)
            {
                timer.Stop();
                GameOver gameOver = new();
                Hide();
                gameOver.Show();
            }
            else if (gameState == GameState.Win)
            {
                timer.Stop();
                YouWin youWin = new();
                Hide();
                youWin.Show();
            }
        }
    }
}
