using System.Collections.Generic;
using System.Linq;

namespace CookPC.VM {
    class Lexer {
        public static List<string> Tokenize(string input) {
            // TODO: Make it fancier
            return input.Split(' ').ToList();
        }
    }
}