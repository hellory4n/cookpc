using Godot;
using System;
using System.Collections.Generic;
using CookPC.VM;

public class Game : Node2D {
	#pragma warning disable 649
	// We assign this in the editor, so we don't need the warning about not being assigned
	[Export]
	public PackedScene ProgramScene;
	#pragma warning restore 649
	private Color[] colors = Init.CookPcInit();
	private Dictionary<Vector2, int> pixels = new Dictionary<Vector2, int>();
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

		this.Update();
	}
  
	public override void _Draw() {
		foreach (KeyValuePair<Vector2, int> pixel in pixels) {
			this.DrawLine(pixel.Key, new Vector2(pixel.Key.x+1, pixel.Key.y+1), colors[pixel.Value]);
		}
	}
}
