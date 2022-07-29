using System.Collections.Generic;
using System.Linq;

namespace CookPC.VM {
    class InstructionRunner {
        public static void Run(List<string> instruction) {
            var method = instruction[0];
            var args = instruction.Skip(1).ToList();

            switch (method) {
                case "testing":
                    System.Console.WriteLine("jcwjjsjhjsj");
                    break;

                case "tests":
                    System.Console.WriteLine("qttiriirieie");
                    break;
            }
        }
    }
}