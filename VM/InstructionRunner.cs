using System.Collections.Generic;
using System.Linq;

namespace CookPC.VM {
    class InstructionRunner {
        public static (List<Dictionary<string, dynamic>>, int, int) Run(List<string> instruction, List<Dictionary<string, dynamic>> memory, int currentChunk, int variableCount) {
            var method = instruction[0];
            var args = instruction.Skip(1).ToList();

            switch (method) {
                #region Memory instructions

                case "mcalloc":
                    // Mega complicated logic.
                    memory.Add(new Dictionary<string, dynamic>());
                    break;

                case "mcset":
                    // int arg0: chunk id
                    var _ = int.TryParse(args[0], out int chunkID);
                    currentChunk = chunkID;
                    break;
                
                case "mvdef":
                    // str arg0: variable name
                    // any arg1: value

                    if (!memory[currentChunk].ContainsKey(args[0]))
                        variableCount++;
                    memory[currentChunk][args[0]] = (dynamic)args[1];
                    break;
                
                case "mvfree":
                    // str arg0: variable name

                    memory[currentChunk].Remove(args[0]);
                    variableCount--;
                    break;

                #endregion
            }

            return (memory, currentChunk, variableCount);
        }
    }
}