using static BattleCityGUI.Internal_Code.GUIGlobals;
using static Battle_City.Internal_Code.Globals;

namespace BattleCityGUI
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Width = FieldWidth * CellWidth;
            this.Height = FieldHeight * CellHeight;
        }
    }
}