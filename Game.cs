using Godot;
using System;
using CookPC.VM;

public class Game : Node2D {
	// CookPC uses a while loop when running scripts so it's not ridiculously slow
	private int loopCounter = 0;
	// TODO: Get this from the cookpc filesystem
	private string bootScript = 
@"testing h
tests h
testing h
tests h
testing h
tests h
testing h
tests h";
	private int maxInstruction;
	private int currentInstruction = 0;
	private string instruction;

	public override void _Ready() {
		maxInstruction = bootScript.Split("\n").Length;
		instruction = bootScript.Split("\n")[currentInstruction];
		GD.Print("hi mom");
	}

	public override void _Process(float delta) {
		while (loopCounter < 500) {
				var jsssjjsjshshsj = Lexer.Tokenize(instruction);
				InstructionRunner.Run(jsssjjsjshshsj);

				currentInstruction++;
				if (currentInstruction == maxInstruction)
					currentInstruction = 0;

				instruction = bootScript.Split("\n")[currentInstruction];

				loopCounter++;
			}
			loopCounter = 0;
	}
}
