using Godot;
using System;
using System.Collections.Generic;
using CookPC.VM;

public class Game : Node2D {
	// CookPC uses a while loop when running scripts so it's not ridiculously slow
	private int loopCounter = 0;
	// TODO: Get this from the cookpc filesystem
	private string bootScript = 
@"mcalloc 0
mcset 0
mvdef pain 69";
	private int maxInstruction;
	private int currentInstruction = 0;
	private string instruction;
	private List<Dictionary<string, dynamic>> memory = new List<Dictionary<string, dynamic>>();
	private int currentChunk;
	private int variableCount = 0;
	private Color[] colors = Init.CookPcInit();

	public override void _Ready() {
		maxInstruction = bootScript.Split("\n").Length;
		instruction = bootScript.Split("\n")[currentInstruction];
		GD.Print("hi mom");
		GD.Print(colors[0]);
		GD.Print(colors[255]);
		this.SetProcess(true);
	}

	public override void _Process(float delta) {
		// Run stuff :)
		// TODO: Make it blazingly fast
		while (loopCounter < 60) {
			var jsssjjsjshshsj = Lexer.Tokenize(instruction, memory, currentChunk);
			/*foreach (var item in jsssjjsjshshsj) {
				System.Console.WriteLine(item);
			}*/
			(memory, currentChunk, variableCount, currentInstruction) = InstructionRunner.Run(jsssjjsjshshsj, memory, currentChunk, variableCount, currentInstruction);

			currentInstruction++;
			if (currentInstruction == maxInstruction)
				currentInstruction = 0;

			instruction = bootScript.Split("\n")[currentInstruction];

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

	public override void _Draw() {
		//this.DrawLine(new Vector2(x, y), new Vector2(x+40, y+40), new Color(0.69f, 0.13f, 0.37f));
	}
}
