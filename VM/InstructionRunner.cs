using System.Collections.Generic;
using System.Linq;
using Godot;

namespace CookPC.VM {
    class InstructionRunner {
        public static void Run(List<string> instruction) {
            var method = instruction[0];
            var args = instruction.Skip(1).ToList();

            switch (method) {
                case "testing":
                    GD.Print("oh ma gawd");
                    break;

                case "tests":
                    GD.Print("the testing tests work");
                    break;
            }
        }
    }
}