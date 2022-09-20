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
	private List<Dictionary<string, dynamic>> memory = new List<Dictionary<string, dynamic>>();
	private int currentChunk;
	private Global global;

	public override void _Ready() {
		maxInstruction = Script.Split("\n").Length;
		instruction = Script.Split("\n")[currentInstruction];
		global = GetNode<Global>("/root/Global");
		GD.Print(this.Name + " is ready");
		this.SetProcess(true);
	}

	public override void _Process(float delta) {
		// Run stuff :)
		// TODO: Make it blazingly fast
		while (loopCounter < 1) {
			var jsssjjsjshshsj = Lexer.Tokenize(instruction, memory, currentChunk);
			(memory, currentChunk, global.VariableCount, currentInstruction, global.Pixels) = InstructionRunner.Run(jsssjjsjshshsj, memory, currentChunk, global.VariableCount, currentInstruction, global.Pixels);

			currentInstruction++;
			if (currentInstruction == maxInstruction)
				currentInstruction = 0;

			instruction = Script.Split("\n")[currentInstruction];

			loopCounter++;
		}

		/*foreach (KeyValuePair<string, dynamic> m in memory[currentChunk]) {
			System.Console.WriteLine(m.Key + ": " + m.Value);
		}*/

		loopCounter = 0;
		
		this.Update();
	}
}
