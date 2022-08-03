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
                
                case "mcamount":
                    // str arg0: the variable the result will be saved to

                    memory[currentChunk][args[0]] = memory.Count;
                    break;
                
                case "mvamount":
                    // int arg0: chunk id
                    // str arg1: the variable the result will be saved to
                    var __ = int.TryParse(args[0], out int cHashtag);
                    if (cHashtag == -1)
                        memory[currentChunk][args[1]] = variableCount;
                    else
                        memory[currentChunk][args[1]] = memory[cHashtag].Count;

                    break;

                #endregion

                #region Misc

                case "debug":
                    System.Console.WriteLine(args[0]);
                    break;

                #endregion

                #region Boolean stuff

                case "not":
                    var ___ = bool.TryParse(args[0], out bool one);
                    memory[currentChunk][args[1]] = !one;
                    break;
                
                case "and":
                    var ____ = bool.TryParse(args[0], out bool two);
                    var _____ = bool.TryParse(args[1], out bool three);
                    memory[currentChunk][args[2]] = two && three;
                    break;
                
                case "or":
                    var ______ = bool.TryParse(args[0], out bool four);
                    var _______ = bool.TryParse(args[1], out bool five);
                    memory[currentChunk][args[2]] = four || five;
                    break;
                
                case "equal":
                    memory[currentChunk][args[2]] = args[0].ToString() == args[1].ToString();
                    break;

                #endregion
            }

            return (memory, currentChunk, variableCount);
        }
    }
}