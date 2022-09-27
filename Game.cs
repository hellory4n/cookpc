using Godot;
using System;
using System.Collections.Generic;
using CookPC.VM;

public class Game : Node2D {
	// TODO: create a program node using a script file in user://
	private Global global;

	public override void _Ready() {
		global = GetNode<Global>("/root/Global");
		/*get_tree().set_screen_stretch(
			SceneTree.STRETCH_MODE_2D, SceneTree.STRETCH_ASPECT_KEEP, settings.resolution
		)*/
		Vector2 resolution = new Vector2(global.Settings.ScreenWidth, global.Settings.ScreenHeight);
		GetTree().SetScreenStretch(
			SceneTree.StretchMode.Viewport, SceneTree.StretchAspect.Keep, resolution
		);
		OS.WindowSize = resolution;
		this.SetProcess(true);
	}

	public override void _Process(float delta) {
		if (global.VariableCount > global.Settings.VariableLimit)
			GetTree().Quit();
		
		// We can't call AddChild from the interpreter part 3
		foreach (ProgramRunner yes in global.NewPrograms) {
			AddChild(yes);
		}

		if (global.NewPrograms.Count > 0)
			global.NewPrograms.Clear();
		
		if (Input.IsKeyPressed((int)KeyList.F11)) {
			OS.WindowFullscreen = !OS.WindowFullscreen;
		}

		this.Update();
	}
  
	public override void _Draw() {
		foreach (KeyValuePair<Vector2, int> pixel in global.Pixels) {
			this.DrawLine(pixel.Key, new Vector2(pixel.Key.x+1, pixel.Key.y+1), global.Colors[pixel.Value]);
		}
	}
}
