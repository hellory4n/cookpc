using Godot;
using System;
using System.Collections.Generic;

public class Global : Node2D {
	public int VariableCount;
	public Dictionary<Vector2, int> Pixels = new Dictionary<Vector2, int>();
	public List<Dictionary<string, dynamic>> Memory = new List<Dictionary<string, dynamic>>();
	public PackedScene ProgramScene = ResourceLoader.Load("res://Program.tscn") as PackedScene;
	// We can't call AddChild from the interpreter part 1
	public List<ProgramRunner> NewPrograms = new List<ProgramRunner>();

	public override void _Ready() {
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta) {
//      
//  }
}
