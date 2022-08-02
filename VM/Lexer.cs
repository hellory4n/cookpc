using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace CookPC.VM {
    class Lexer {
        public static List<string> Tokenize(string input) {
            Regex regex = new Regex(@"[ ](?=(?:[^""]*""[^""]*"")*[^""]*$)", RegexOptions.Multiline);
            string[] splits = regex.Split(input);
            List<string> splits2 = new List<string>();

            foreach (var item in splits) {
                var jsssjjsjshshsj = item;
                if (jsssjjsjshshsj.StartsWith("\"") && jsssjjsjshshsj.EndsWith("\""))
                    jsssjjsjshshsj = DoStuffWithString(item);
                splits2.Add(jsssjjsjshshsj);
            }

            return splits2;
        }

        // No idea why I made this a separate method
        // TODO: Write good code
        public static string DoStuffWithString(string input) {
            // TODO: Add the \ thing
            if (input.StartsWith("\"") && input.EndsWith("\""))
                input = input.Substring(1, input.Length-2);
            return input;
        }
    }
}