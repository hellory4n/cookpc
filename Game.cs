using Godot;
using System;
using System.Collections.Generic;
using CookPC.VM;

public class Game : Node2D {
	// TODO: Get this from the cookpc filesystem
	// sorry for the massive string
	private string bootScript = 
@"mcalloc 0
mcalloc 0
mcset 0
mvdef c 0
add $c 1 c
debug $c
equal $c 10 urmom
not $urmom urmom
ifjump 4 $urmom";
	private int maxInstruction;
	private List<Dictionary<string, dynamic>> memory = new List<Dictionary<string, dynamic>>();
	private int currentChunk;
	private int variableCount = 0;
	private Color[] colors = Init.CookPcInit();
	private Dictionary<Vector2, int> pixels = new Dictionary<Vector2, int>();
	private string[] instructions;
	private string instruction;

	public override void _Ready() {
		maxInstruction = bootScript.Split("\n").Length;
		instructions = bootScript.Split("\n");
		GD.Print("everything is ready");
		this.SetProcess(true);
	}

	public override void _Process(float delta) {
		// Run stuff :)
		// I was gonna use a foreach loop if it let me change currentInstruction
		for (int currentInstruction = 0; currentInstruction < maxInstruction; currentInstruction++) {
			instruction = instructions[currentInstruction];

			var jsssjjsjshshsj = Lexer.Tokenize(instruction, memory, currentChunk);
			/*foreach (var item in jsssjjsjshshsj) {
				System.Console.WriteLine(item);
			}*/
			(memory, currentChunk, variableCount, currentInstruction, pixels) = InstructionRunner.Run(jsssjjsjshshsj, memory, currentChunk, variableCount, currentInstruction, pixels);

			currentInstruction++;
			if (currentInstruction == maxInstruction)
				currentInstruction = 0;
		}

		/*foreach (KeyValuePair<string, dynamic> m in memory[currentChunk]) {
			System.Console.WriteLine(m.Key + ": " + m.Value);
		}*/

		// We don't want people to create 694201337 variables, that's illegal
		// TODO: Get this from somewhere
		// TODO: Don't make the limit this stupid
		if (variableCount > 9) 
			GetTree().Quit();
		
		this.Update();
	}

	public override void _Draw() {
		foreach (KeyValuePair<Vector2, int> pixel in pixels) {
			this.DrawLine(pixel.Key, new Vector2(pixel.Key.x+1, pixel.Key.y+1), colors[pixel.Value]);
		}
	}
}
