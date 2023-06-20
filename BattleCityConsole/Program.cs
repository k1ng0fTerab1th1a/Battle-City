using Battle_City.Engines;
using System.Diagnostics.SymbolStore;

namespace Battle_City
{
    internal static class Program
    {
        private static void Main()
        {
            GameEngine engine = new GameEngine();
            GraphicsEngine graphicsEngine = new GraphicsEngine();
            graphicsEngine.Set();
            while (true)
            {
                engine.Run();
            }
        }
    }
}