using Godot;
using System;
using System.Collections.Generic;
using CookPC.VM;

public class Game : Node2D {
	// CookPC uses a while loop when running scripts so it's not ridiculously slow
	private int loopCounter = 0;
	// TODO: Get this from the cookpc filesystem
	// sorry for the massive string
	private string bootScript = 
@"paint 0 0 0
paint 1 0 1
paint 2 0 2
paint 3 0 3
paint 4 0 4
paint 5 0 5
paint 6 0 6
paint 7 0 7
paint 8 0 8
paint 9 0 9
paint 10 0 10
paint 11 0 11
paint 12 0 12
paint 13 0 13
paint 14 0 14
paint 15 0 15
paint 0 1 16
paint 1 1 17
paint 2 1 18
paint 3 1 19
paint 4 1 20
paint 5 1 21
paint 6 1 22
paint 7 1 23
paint 8 1 24
paint 9 1 25
paint 10 1 26
paint 11 1 27
paint 12 1 28
paint 13 1 29
paint 14 1 30
paint 15 1 31
paint 0 2 32
paint 1 2 33
paint 2 2 34
paint 3 2 35
paint 4 2 36
paint 5 2 37
paint 6 2 38
paint 7 2 39
paint 8 2 40
paint 9 2 41
paint 10 2 42
paint 11 2 43
paint 12 2 44
paint 13 2 45
paint 14 2 46
paint 15 2 47
paint 0 3 48
paint 1 3 49
paint 2 3 50
paint 3 3 51
paint 4 3 52
paint 5 3 53
paint 6 3 54
paint 7 3 55
paint 8 3 56
paint 9 3 57
paint 10 3 58
paint 11 3 59
paint 12 3 60
paint 13 3 61
paint 14 3 62
paint 15 3 63
paint 0 4 64
paint 1 4 65
paint 2 4 66
paint 3 4 67
paint 4 4 68
paint 5 4 69
paint 6 4 70
paint 7 4 71
paint 8 4 72
paint 9 4 73
paint 10 4 74
paint 11 4 75
paint 12 4 76
paint 13 4 77
paint 14 4 78
paint 15 4 79
paint 0 5 80
paint 1 5 81
paint 2 5 82
paint 3 5 83
paint 4 5 84
paint 5 5 85
paint 6 5 86
paint 7 5 87
paint 8 5 88
paint 9 5 89
paint 10 5 90
paint 11 5 91
paint 12 5 92
paint 13 5 93
paint 14 5 94
paint 15 5 95
paint 0 6 96
paint 1 6 97
paint 2 6 98
paint 3 6 99
paint 4 6 100
paint 5 6 101
paint 6 6 102
paint 7 6 103
paint 8 6 104
paint 9 6 105
paint 10 6 106
paint 11 6 107
paint 12 6 108
paint 13 6 109
paint 14 6 110
paint 15 6 111
paint 0 7 112
paint 1 7 113
paint 2 7 114
paint 3 7 115
paint 4 7 116
paint 5 7 117
paint 6 7 118
paint 7 7 119
paint 8 7 120
paint 9 7 121
paint 10 7 122
paint 11 7 123
paint 12 7 124
paint 13 7 125
paint 14 7 126
paint 15 7 127
paint 0 8 128
paint 1 8 129
paint 2 8 130
paint 3 8 131
paint 4 8 132
paint 5 8 133
paint 6 8 134
paint 7 8 135
paint 8 8 136
paint 9 8 137
paint 10 8 138
paint 11 8 139
paint 12 8 140
paint 13 8 141
paint 14 8 142
paint 15 8 143
paint 0 9 144
paint 1 9 145
paint 2 9 146
paint 3 9 147
paint 4 9 148
paint 5 9 149
paint 6 9 150
paint 7 9 151
paint 8 9 152
paint 9 9 153
paint 10 9 154
paint 11 9 155
paint 12 9 156
paint 13 9 157
paint 14 9 158
paint 15 9 159
paint 0 10 160
paint 1 10 161
paint 2 10 162
paint 3 10 163
paint 4 10 164
paint 5 10 165
paint 6 10 166
paint 7 10 167
paint 8 10 168
paint 9 10 169
paint 10 10 170
paint 11 10 171
paint 12 10 172
paint 13 10 173
paint 14 10 174
paint 15 10 175
paint 0 11 176
paint 1 11 177
paint 2 11 178
paint 3 11 179
paint 4 11 180
paint 5 11 181
paint 6 11 182
paint 7 11 183
paint 8 11 184
paint 9 11 185
paint 10 11 186
paint 11 11 187
paint 12 11 188
paint 13 11 189
paint 14 11 190
paint 15 11 191
paint 0 12 192
paint 1 12 193
paint 2 12 194
paint 3 12 195
paint 4 12 196
paint 5 12 197
paint 6 12 198
paint 7 12 199
paint 8 12 200
paint 9 12 201
paint 10 12 202
paint 11 12 203
paint 12 12 204
paint 13 12 205
paint 14 12 206
paint 15 12 207
paint 0 13 208
paint 1 13 209
paint 2 13 210
paint 3 13 211
paint 4 13 212
paint 5 13 213
paint 6 13 214
paint 7 13 215
paint 8 13 216
paint 9 13 217
paint 10 13 218
paint 11 13 219
paint 12 13 220
paint 13 13 221
paint 14 13 222
paint 15 13 223
paint 0 14 224
paint 1 14 225
paint 2 14 226
paint 3 14 227
paint 4 14 228
paint 5 14 229
paint 6 14 230
paint 7 14 231
paint 8 14 232
paint 9 14 233
paint 10 14 234
paint 11 14 235
paint 12 14 236
paint 13 14 237
paint 14 14 238
paint 15 14 239
paint 0 15 240
paint 1 15 241
paint 2 15 242
paint 3 15 243
paint 4 15 244
paint 5 15 245
paint 6 15 246
paint 7 15 247
paint 8 15 248
paint 9 15 249
paint 10 15 250
paint 11 15 251
paint 12 15 252
paint 13 15 253
paint 14 15 254
paint 15 15 255
dclear";
	private int maxInstruction;
	private int currentInstruction = 0;
	private string instruction;
	private List<Dictionary<string, dynamic>> memory = new List<Dictionary<string, dynamic>>();
	private int currentChunk;
	private int variableCount = 0;
	private Color[] colors = Init.CookPcInit();
	private Dictionary<Vector2, int> pixels = new Dictionary<Vector2, int>();

	public override void _Ready() {
		maxInstruction = bootScript.Split("\n").Length;
		instruction = bootScript.Split("\n")[currentInstruction];
		GD.Print("everything is ready");
		this.SetProcess(true);
	}

	public override void _Process(float delta) {
		// Run stuff :)
		// TODO: Make it blazingly fast
		while (loopCounter < 1) {
			var jsssjjsjshshsj = Lexer.Tokenize(instruction, memory, currentChunk);
			/*foreach (var item in jsssjjsjshshsj) {
				System.Console.WriteLine(item);
			}*/
			(memory, currentChunk, variableCount, currentInstruction, pixels) = InstructionRunner.Run(jsssjjsjshshsj, memory, currentChunk, variableCount, currentInstruction, pixels);

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
		foreach (KeyValuePair<Vector2, int> pixel in pixels) {
			this.DrawLine(pixel.Key, new Vector2(pixel.Key.x+1, pixel.Key.y+1), colors[pixel.Value]);
		}
	}
}
