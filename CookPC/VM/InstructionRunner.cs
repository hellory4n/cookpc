using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;

namespace CookPC.VM {
    class InstructionRunner {
        public static (List<Dictionary<string, dynamic>>, int, int, int, Dictionary<Vector2, int>) Run(List<string> instruction, List<Dictionary<string, dynamic>> memory, int currentChunk, int variableCount, int currentInstruction, string cookfolder, Computer pc, Dictionary<Vector2, int> pixels) {
            var method = instruction[0];
            var args = instruction.Skip(1).ToList();

            switch (method) {
                #region Memory instructions

                // memory chunk allocate
                case "mcalloc":
                    // Mega complicated logic.
                    memory.Add(new Dictionary<string, dynamic>());
                    break;

                // memory chunk set
                case "mcset":
                    // int arg0: chunk id
                    var _ = int.TryParse(args[0], out int chunkID);
                    currentChunk = chunkID;
                    break;
                
                // memory variable define
                case "mvdef":
                    // str arg0: variable name
                    // any arg1: value

                    if (!memory[currentChunk].ContainsKey(args[0]))
                        variableCount++;
                    memory[currentChunk][args[0]] = (dynamic)args[1];
                    break;
                
                // memory variable free
                case "mvfree":
                    // str arg0: variable name

                    memory[currentChunk].Remove(args[0]);
                    variableCount--;
                    break;
                
                // memory chunk amount
                case "mcamount":
                    // str arg0: the variable the result will be saved to

                    memory[currentChunk][args[0]] = memory.Count;
                    break;
                
                // memory variable amount
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

                // string combine                
                case "strcomb":
                    memory[currentChunk][args[2]] = args[0].ToString() + args[1].ToString();
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
                
                case "less":
                    var ________ = double.TryParse(args[0], out double six);
                    var _________ = double.TryParse(args[1], out double seven);
                    memory[currentChunk][args[2]] = six < seven;
                    break;
                
                case "greater":
                    var __________ = double.TryParse(args[0], out double eight);
                    var ___________ = double.TryParse(args[1], out double nine);
                    memory[currentChunk][args[2]] = eight > nine;
                    break;

                #endregion

                #region Me- uhh i mean math

                case "add":
                    var ____________ = double.TryParse(args[0], out double ten);
                    var _____________ = double.TryParse(args[1], out double eleven);
                    memory[currentChunk][args[2]] = ten + eleven;
                    break;
                
                case "subtract":
                    var ______________ = double.TryParse(args[0], out double twelve);
                    var _______________ = double.TryParse(args[1], out double thirteen);
                    memory[currentChunk][args[2]] = twelve - thirteen;
                    break;
                
                case "multiply":
                    var ________________ = double.TryParse(args[0], out double fourteen);
                    var _________________ = double.TryParse(args[1], out double fifteen);
                    memory[currentChunk][args[2]] = fourteen * fifteen;
                    break;
                
                case "divide":
                    var __________________ = double.TryParse(args[0], out double sixteen);
                    var ___________________ = double.TryParse(args[1], out double seventeen);
                    memory[currentChunk][args[2]] = sixteen / seventeen;
                    break;
                
                case "modulo":
                    var ____________________ = double.TryParse(args[0], out double eighteen);
                    var _____________________ = double.TryParse(args[1], out double nineteen);
                    memory[currentChunk][args[2]] = eighteen % nineteen;
                    break;
                
                case "power":
                    var ______________________ = double.TryParse(args[0], out double twenty);
                    var _______________________ = double.TryParse(args[1], out double twentyone);
                    memory[currentChunk][args[2]] = Math.Pow(twenty, twentyone);
                    break;

                #endregion

                #region Control

                case "jump":
                    var ________________________ = int.TryParse(args[0], out int twentytwo);
                    currentInstruction = twentytwo-1; // the interpreter will increment the currentInstruction counter
                    break;
                
                case "ifjump":
                    var _________________________ = int.TryParse(args[0], out int twentythree);
                    var __________________________ = bool.TryParse(args[1], out bool twentyfour);
                    if (twentyfour)
                        currentInstruction = twentythree-1; // the interpreter will increment the currentInstruction counter
                    break;

                #endregion

                #region PC info

                case "pcversion":
                    memory[currentChunk][args[0]] = pc.version;
                    break;
                
                case "pcvertype":
                    memory[currentChunk][args[0]] = pc.versionType;
                    break;
                
                case "pcarch":
                    memory[currentChunk][args[0]] = pc.cpuArchitecture;
                    break;
                
                case "pcarchver":
                    memory[currentChunk][args[0]] = pc.cpuArchitectureVersion;
                    break;
                
                case "pcipf":
                    memory[currentChunk][args[0]] = pc.instructionsPerFrame;
                    break;
                
                case "pctvcl":
                    memory[currentChunk][args[0]] = pc.totalVariableCountLimit;
                    break;

                #endregion

                #region Storage

                case "swrite":
                    // str arg0: disk to write
                    // int arg1: position on disk
                    // str arg2: character

                    string old = File.ReadAllText(cookfolder + "/" + args[0]);
                    StringBuilder sb = new StringBuilder(old);
                    var ___________________________ = int.TryParse(args[1], out int twentyfive);
                    var i = 0;
 
                    foreach (var item in args[2]) {
                        sb[twentyfive+i] = item;
                        i++;
                    }

                    var g = sb.ToString(); // TODO: be a good programmer
                    File.WriteAllText(cookfolder + "/" + args[0], g);

                    break;
                
                case "sread":
                    // str arg0: disk to write
                    // int arg1: start position
                    // int arg2: end position
                    // str arg3: save to

                    string data = File.ReadAllText(cookfolder + "/" + args[0]);
                    StringBuilder sb2 = new StringBuilder(data);
                    var ____________________________ = int.TryParse(args[1], out int twentysix);
                    var _____________________________ = int.TryParse(args[2], out int twentyseven);
                    var ii = 0;
                    string thething = "";
 
                    while (ii != twentyseven+1) {
                        thething += sb2[twentysix+ii];
                        ii++;
                    }

                    memory[currentChunk][args[3]] = thething;

                    break;

                #endregion

                #region Display

                case "paint":
                    // int arg0: x
                    // int arg1: y
                    // int arg2: color

                    var ______________________________ = int.TryParse(args[0], out int x);
                    var _______________________________ = int.TryParse(args[1], out int y);
                    var ________________________________ = int.TryParse(args[2], out int colros);
                    pixels[new Vector2(x, y)] = colros;
                    break;
                
                // display clear
                case "dclear":
                    pixels.Clear();
                    break;

                #endregion
            }

            return (memory, currentChunk, variableCount, currentInstruction, pixels);
        }
    }
}