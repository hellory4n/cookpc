using Godot;
using System;
using System.Collections.Generic;
using CookPC.VM;

public class Game : Node2D {
	public Color[] colors = Init.CookPcInit();
	// TODO: create a program node using a script file in user://
	private Global global;

	public override void _Ready() {
		global = GetNode<Global>("/root/Global");
		this.SetProcess(true);
	}

	public override void _Process(float delta) {
		// TODO: Don't make the limit this stupid
		// TODO: Get the limit from the json settings file thing
		if (global.VariableCount > 10)
			GetTree().Quit();
		
		// We can't call AddChild from the interpreter part 3
		foreach (ProgramRunner yes in global.NewPrograms) {
			AddChild(yes);
		}
		if (global.NewPrograms.Count > 0)
			global.NewPrograms.Clear();

		this.Update();
	}
  
	public override void _Draw() {
		foreach (KeyValuePair<Vector2, int> pixel in global.Pixels) {
			this.DrawLine(pixel.Key, new Vector2(pixel.Key.x+1, pixel.Key.y+1), colors[pixel.Value]);
		}
	}
}
