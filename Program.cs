using System;

namespace CookPC {
    public static class Program  {
        [STAThread]
        static void Main() {
            using (var game = new Game1())
                game.Run();
        }
    }
}
