using Godot;
using System;
using System.Collections.Generic;

public class Global : Node2D {
	public int VariableCount;
	public Dictionary<Vector2, int> Pixels = new Dictionary<Vector2, int>();

	public override void _Ready() {
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta) {
//      
//  }
}
