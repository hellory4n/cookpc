
using System.Collections.Generic;
using System.Linq;
using System;
using Godot;

namespace CookPC.VM {
    class InstructionRunner {
        public static (
            List<Dictionary<string, dynamic>>,
            int,
            int,
            int,
            Dictionary<Vector2, int>,
            List<ProgramRunner>,
            int
        ) Run(
            List<string> instruction,
            List<Dictionary<string, dynamic>> memory,
            int currentChunk,
            int variableCount,
            int currentInstruction,
            Dictionary<Vector2, int> pixels,
            PackedScene programScene,
            List<ProgramRunner> newPrograms,
            int cpuCycles
        ) {
            #region Do stuff the lexer can't do
            List<string> newInstruction = new List<string>();
            foreach (var item in instruction) {
				var jsssjjsjshshsj = item;

				if (!jsssjjsjshshsj.StartsWith("\"") && jsssjjsjshshsj.StartsWith("$")) {
                    var theThingWithoutTheDollarSign = item.Substring(1, item.Length-1);
                    jsssjjsjshshsj = memory[currentChunk][theThingWithoutTheDollarSign].ToString(); // you're welcome
				}

				newInstruction.Add(jsssjjsjshshsj);
			}
            #endregion

            var method = newInstruction[0];
            var args = newInstruction.Skip(1).ToList();

            switch (method) {
                #region Memory instructions

                // Memory Chunk ALLOCate
                case "mcalloc":
                    // Mega complicated logic.
                    memory.Add(new Dictionary<string, dynamic>());
                    break;

                // Memory Chunk SET
                case "mcset":
                    // int arg0: chunk id
                    var _ = int.TryParse(args[0], out int chunkID);
                    currentChunk = chunkID;
                    break;
                
                // Memory Variables DEFine
                case "mvdef":
                    // str arg0: variable name
                    // any arg1: value

                    if (!memory[currentChunk].ContainsKey(args[0]))
                        variableCount++;
                    memory[currentChunk][args[0]] = (dynamic)args[1];
                    break;
                
                // Memory Variables FREE
                case "mvfree":
                    // str arg0: variable name

                    memory[currentChunk].Remove(args[0]);
                    variableCount--;
                    break;
                
                // Memory Chunk AMOUNT
                case "mcamount":
                    // str arg0: the variable the result will be saved to

                    memory[currentChunk][args[0]] = memory.Count;
                    break;
                
                // Memory Chunk AMOUNT
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
                    GD.Print(args[0]);
                    break;

                // STRing COMBine                
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
                
                case "run":
                    // str arg0: the script this program will run

                    ProgramRunner newProgram = (ProgramRunner)programScene.Instance();
                    newProgram.Init(args[0]);
                    // We can't call AddChild from the interpreter part 2
                    newPrograms.Add(newProgram);
                    break;
                
                case "morecpu":
                    // int arg0: how many instructions this program will now run every frame
                    var ___________________________ = int.TryParse(args[0], out int twentyfive);
                    cpuCycles = twentyfive;
                    break;

                #endregion
                #region Display

                case "paint":
                    // int arg0: x
                    // int arg1: y
                    // int arg2: color id (starts at 0)
                    var ______________________________ = int.TryParse(args[0], out int x);
                    var _______________________________ = int.TryParse(args[1], out int y);
                    var ________________________________ = int.TryParse(args[2], out int color);
                    pixels[new Vector2(x, y)] = color;
                    break;
                
                // Display CLEAR
                case "dclear":
                    pixels.Clear();
                    break;

                #endregion
            }

            return (memory, currentChunk, variableCount, currentInstruction, pixels, newPrograms, cpuCycles);
        }
    }
}