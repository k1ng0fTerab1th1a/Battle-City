using static BattleCityGUI.Internal_Code.GUIGlobals;
using static Battle_City.Internal_Code.Globals;
using BattleCityGUI.Properties;
using BattleCityGUI;

namespace BattleCityGUI.Forms
{
    public partial class FormBase : Form
    {
        public FormBase()
        {
            InitializeComponent();
            this.Width = FieldWidth * CellWidth + CellWidth;
            this.Height = FieldHeight * CellHeight + 36;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosed += OnClose;
        }

        public void OnClose(object? sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
