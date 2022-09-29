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
	private List<string> instruction;
	private int currentChunk = 0;
	private Global global;
	private string[] instructionList;
	private List<List<string>> instructions = new List<List<string>>();

	// We need this for the run instruction
	public void Init(string script) {
		Script = script;
	}

	public override void _Ready() {
		instructionList = Script.Split("\n");
		maxInstruction = instructionList.Length;

		// Tokenize the thing
		foreach (string thething in instructionList) {
			instructions.Add(Lexer.Tokenize(thething));
		}
		instruction = instructions[currentInstruction];

		global = GetNode<Global>("/root/Global");
		GD.Print(this.Name + " is ready");
		this.SetProcess(true);
	}

	public override void _Process(float delta) {
		// Run stuff :)
		while (loopCounter < CpuCycles) {
			// sorry
			(global.Memory, currentChunk, global.VariableCount, currentInstruction, global.Pixels, global.NewPrograms, CpuCycles) = InstructionRunner.Run(instruction, global.Memory, currentChunk, global.VariableCount, currentInstruction, global.Pixels, global.ProgramScene, global.NewPrograms, CpuCycles);

			currentInstruction++;
			if (currentInstruction == maxInstruction)
				currentInstruction = 0;

			instruction = instructions[currentInstruction];

			loopCounter++;
		}

		/*foreach (KeyValuePair<string, dynamic> m in memory[currentChunk]) {
			System.Console.WriteLine(m.Key + ": " + m.Value);
		}*/

		loopCounter = 0;
		
		this.Update();
	}
}
