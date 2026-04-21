using static CodeBase.Internal_Code.Globals;
using CodeBase.Internal_Code;
using CodeBase.Fields;
using CodeBase.Game_Elements;
using CodeBase.Game_Elements.Cells;
using CodeBase.Game_Elements.Entities;

namespace BattleCityGUI
{
    internal static class Program
    {
        static void Main()
        {
            GraphicsEngine graphicsEngine = new GraphicsEngine();
            graphicsEngine.Set();
            Application.Run(new StartMenu());
        }
    }
}