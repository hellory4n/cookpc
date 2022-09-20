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
	private int variableCount = 0;
	private Color[] colors = Init.CookPcInit();
	private Dictionary<Vector2, int> pixels = new Dictionary<Vector2, int>();

	public override void _Ready() {
		maxInstruction = Script.Split("\n").Length;
		instruction = Script.Split("\n")[currentInstruction];
		GD.Print(this.Name + " is ready");
		this.SetProcess(true);
	}

	public override void _Process(float delta) {
		// Run stuff :)
		// TODO: Make it blazingly fast
		while (loopCounter < 1) {
			var jsssjjsjshshsj = Lexer.Tokenize(instruction, memory, currentChunk);
			(memory, currentChunk, variableCount, currentInstruction, pixels) = InstructionRunner.Run(jsssjjsjshshsj, memory, currentChunk, variableCount, currentInstruction, pixels);

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

		// We don't want people to create 694201337 variables, that's illegal
		// TODO: Get this from somewhere
		// TODO: Don't make the limit this stupid
		if (variableCount > 9) 
			GetTree().Quit();
		
		this.Update();
	}
}
