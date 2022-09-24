using Godot;
using System;
using System.Collections.Generic;
using CookPC.VM;

public class Global : Node2D {
	public int VariableCount;
	public Dictionary<Vector2, int> Pixels = new Dictionary<Vector2, int>();
	public List<Dictionary<string, dynamic>> Memory = new List<Dictionary<string, dynamic>>();
	public PackedScene ProgramScene = ResourceLoader.Load("res://Program.tscn") as PackedScene;
	// We can't call AddChild from the interpreter part 1
	public List<ProgramRunner> NewPrograms = new List<ProgramRunner>();
	public Color[] Colors;
	public Settings Settings;

	public override void _Ready() {
		(Colors, Settings) = Init.CookPcInit();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta) {
//      
//  }
}
