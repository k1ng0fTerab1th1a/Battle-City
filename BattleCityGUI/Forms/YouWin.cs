using static BattleCityGUI.Internal_Code.GUIGlobals;
using static Battle_City.Internal_Code.Globals;
using BattleCityGUI.Properties;
using BattleCityGUI.Forms;

namespace BattleCityGUI
{
    public partial class YouWin : FormBase
    {
        public YouWin() : base()
        {
            this.BackgroundImage = Resources.youWin;
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
