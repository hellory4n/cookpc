using Godot;
using System;
using System.Collections.Generic;
using CookPC.VM;

public class ProgramRunner : Node2D {
	[Export(PropertyHint.MultilineText)]
	public string Script;
	[Export]
	public int CpuCycles = 750;
	// CookPC uses a while loop when running scripts so it's not ridiculously slow
	private int loopCounter = 0;
	private int maxInstruction;
	private int currentInstruction = 0;
	private string instruction;
	private int currentChunk;
	private Global global;
	private string[] instructionList;

	// We need this for the run instructions
	public void Init(string script) {
		Script = script;
	}

	public override void _Ready() {
		instructionList = Script.Split("\n");
		maxInstruction = instructionList.Length;
		instruction = instructionList[currentInstruction];
		global = GetNode<Global>("/root/Global");
		GD.Print(this.Name + " is ready");
		this.SetProcess(true);
	}

	public override void _Process(float delta) {
		// Run stuff :)
		while (loopCounter < CpuCycles) {
			var jsssjjsjshshsj = Lexer.Tokenize(instruction, global.Memory, currentChunk);
			// sorry
			(global.Memory, currentChunk, global.VariableCount, currentInstruction, global.Pixels, global.NewPrograms, CpuCycles) = InstructionRunner.Run(jsssjjsjshshsj, global.Memory, currentChunk, global.VariableCount, currentInstruction, global.Pixels, global.ProgramScene, global.NewPrograms, CpuCycles);

			currentInstruction++;
			if (currentInstruction == maxInstruction)
				currentInstruction = 0;

			instruction = instructionList[currentInstruction];

			loopCounter++;
		}

		/*foreach (KeyValuePair<string, dynamic> m in memory[currentChunk]) {
			System.Console.WriteLine(m.Key + ": " + m.Value);
		}*/

		loopCounter = 0;
		
		this.Update();
	}
}
