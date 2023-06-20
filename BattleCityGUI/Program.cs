using static Battle_City.Internal_Code.Globals;
using Battle_City.Internal_Code;
using Battle_City.Fields;
using Battle_City.Game_Elements;
using Battle_City.Game_Elements.Cells;
using Battle_City.Game_Elements.Entities;
namespace BattleCityGUI
{
    internal static class Program
    {
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var mw = new MainWindow();


            

            Application.Run(mw);
        }
    }
}