using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace CookPC.VM {
    class Lexer {
        public static List<string> Tokenize(string input) {
            Regex regex = new Regex(@"[ ](?=(?:[^""]*""[^""]*"")*[^""]*$)", RegexOptions.Multiline);
            string[] splits = regex.Split(input);
            return splits.ToList();
        }
    }
}