using static BattleCityGUI.Internal_Code.GUIGlobals;
using static Battle_City.Internal_Code.Globals;
using BattleCityGUI.Properties;
using BattleCityGUI.Forms;

namespace BattleCityGUI
{
    public partial class GameOver : FormBase
    {
        public GameOver() : base()
        {
            this.BackgroundImage = Resources.gameOver;
            this.KeyDown += OnKeyPress;
        }

        private void OnKeyPress(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            else
            {
                StartMenu startMenu = new();
                Hide();
                startMenu.Show();
            }
        }
    }
}
