using static BattleCityGUI.Internal_Code.GUIGlobals;
using static Battle_City.Internal_Code.Globals;
using BattleCityGUI.Properties;
using BattleCityGUI.Forms;

namespace BattleCityGUI
{
    public partial class StartMenu : FormBase
    {
        public StartMenu() : base()
        {
            this.BackgroundImage = Resources.startMenu;
            this.KeyDown += OnKeyPress;
        }

        private void OnKeyPress(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            GameForm gameForm = new();
            Hide();
            gameForm.Show();
            gameForm.RunGame();
        }
    }
}