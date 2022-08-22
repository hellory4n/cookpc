using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CookPC.VM;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace CookPC {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int loopCounter = 0;
        // TODO: Get this from the cookpc filesystem
        private string bootScript = 
@"mcalloc 0
mcset 0
paint 0 0 0
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
        // TODO: Test it on android and windows
        string cookfolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/cookpc";
        
        Computer pc;
        Texture2D pixel;
        RenderTarget2D _nativeRenderTarget;
        Dictionary<Vector2, int> pixels = new Dictionary<Vector2, int>();
        string[] colors;

        public Game1() {
            maxInstruction = bootScript.Split("\n").Length;
            instruction = bootScript.Split("\n")[currentInstruction];

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            _graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        protected override void Initialize() {
            if (!Directory.Exists(cookfolder)) {
                Directory.CreateDirectory(cookfolder);
                File.WriteAllText(cookfolder + "/cookpc.json", 
@"{
	""version"": ""0.1"",
	""versionType"": ""DEV"",
	""cpuArchitecture"": ""leg16"",
	""cpuArchitectureVersion"": ""0"",
	""instructionsPerFrame"": 1,
	""totalVariableCountLimit"": 5120,
	""bootSize"": 69,
	""localstorageSize"": 69,
	""floppySize"": 69,
	""screenWidth"": 16,
	""screenHeight"": 16,
	""screenScale"": 35
}");
                string pcJson = File.ReadAllText(cookfolder + "/cookpc.json");
                pc = JsonConvert.DeserializeObject<Computer>(pcJson);

                // TODO: change drive size
                File.WriteAllText(cookfolder + "/boot", new string(' ', pc.bootSize));
                File.WriteAllText(cookfolder + "/localstorage", new string(' ', pc.localstorageSize));
                File.WriteAllText(cookfolder + "/floppy_a", new string(' ', pc.floppySize));
                File.WriteAllText(cookfolder + "/floppy_b", new string(' ', pc.floppySize));
                File.WriteAllText(cookfolder + "/colors",
@"000000
0b0b0b
222222
444444
555555
777777
888888
aaaaaa
bbbbbb
dddddd
eeeeee
00000b
000022
000044
000055
000077
000088
0000aa
0000bb
0000dd
0000ee
000b00
002200
004400
005500
007700
008800
00aa00
00bb00
00dd00
00ee00
0b0000
220000
440000
550000
770000
880000
aa0000
bb0000
dd0000
ee0000
000033
000066
000099
0000cc
0000ff
003300
003333
003366
003399
0033cc
0033ff
006600
006633
006666
006699
0066cc
0066ff
009900
009933
009966
009999
0099cc
0099ff
00cc00
00cc33
00cc66
00cc99
00cccc
00ccff
00ff00
00ff33
00ff66
00ff99
00ffcc
00ffff
330000
330033
330066
330099
3300cc
3300ff
333300
333333
333366
333399
3333cc
3333ff
336600
336633
336666
336699
3366cc
3366ff
339900
339933
339966
339999
3399cc
3399ff
33cc00
33cc33
33cc66
33cc99
33cccc
33ccff
33ff00
33ff33
33ff66
33ff99
33ffcc
33ffff
660000
660033
660066
660099
6600cc
6600ff
663300
663333
663366
663399
6633cc
6633ff
666600
666633
666666
666699
6666cc
6666ff
669900
669933
669966
669999
6699cc
6699ff
66cc00
66cc33
66cc66
66cc99
66cccc
66ccff
66ff00
66ff33
66ff66
66ff99
66ffcc
66ffff
990000
990033
990066
990099
9900cc
9900ff
993300
993333
993366
993399
9933cc
9933ff
996600
996633
996666
996699
9966cc
9966ff
999900
999933
999966
999999
9999cc
9999ff
99cc00
99cc33
99cc66
99cc99
99cccc
99ccff
99ff00
99ff33
99ff66
99ff99
99ffcc
99ffff
cc0000
cc0033
cc0066
cc0099
cc00cc
cc00ff
cc3300
cc3333
cc3366
cc3399
cc33cc
cc33ff
cc6600
cc6633
cc6666
cc6699
cc66cc
cc66ff
cc9900
cc9933
cc9966
cc9999
cc99cc
cc99ff
cccc00
cccc33
cccc66
cccc99
cccccc
ccccff
ccff00
ccff33
ccff66
ccff99
ccffcc
ccffff
ff0000
ff0033
ff0066
ff0099
ff00cc
ff00ff
ff3300
ff3333
ff3366
ff3399
ff33cc
ff33ff
ff6600
ff6633
ff6666
ff6699
ff66cc
ff66ff
ff9900
ff9933
ff9966
ff9999
ff99cc
ff99ff
ffcc00
ffcc33
ffcc66
ffcc99
ffcccc
ffccff
ffff00
ffff33
ffff66
ffff99
ffffcc
ffffff");
            } else {
                string pcJson = File.ReadAllText(cookfolder + "/cookpc.json");
                pc = JsonConvert.DeserializeObject<Computer>(pcJson);
            }

            // Set screen size
            _graphics.PreferredBackBufferWidth = (int)Math.Round(pc.screenWidth * pc.screenScale);
            _graphics.PreferredBackBufferHeight = (int)Math.Round(pc.screenHeight * pc.screenScale);
            
            _graphics.ApplyChanges();

            // Jjjzjjjzhjzhjsss
            colors = File.ReadAllText(cookfolder + "/colors").Split("\n");

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            using (var fs = File.OpenRead("/home/toddynho/GameProjects/CookPC/SourceCode/CookPC/Content/pixel.png"))
                pixel = Texture2D.FromStream(GraphicsDevice, fs);
        }

        protected override void Update(GameTime gameTime) {
            // Run stuff :)
            while (loopCounter < pc.instructionsPerFrame) {
                var jsssjjsjshshsj = Lexer.Tokenize(instruction, memory, currentChunk);
                /*foreach (var item in jsssjjsjshshsj) {
                    System.Console.WriteLine(item);
                }*/
                (memory, currentChunk, variableCount, currentInstruction, pixels) = InstructionRunner.Run(jsssjjsjshshsj, memory, currentChunk, variableCount, currentInstruction, cookfolder, pc, pixels);

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
            if (variableCount > pc.totalVariableCountLimit)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            // Initialization
            _nativeRenderTarget = new RenderTarget2D(GraphicsDevice, pc.screenWidth, pc.screenHeight);

            // Draw call
            GraphicsDevice.SetRenderTarget(_nativeRenderTarget);
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            
            // Draw pixels
            // TODO: Add color
            foreach (var (position, color) in pixels) {
                var rodrigo = colors[color];
                var carlos = Enumerable.Range(0, rodrigo.Length / 2)
        .Select(i => rodrigo.Substring(i * 2, 2));
                var re_d = carlos.ToArray()[0];
                var g_reen = carlos.ToArray()[1];
                var _blue = carlos.ToArray()[2];
                var red = byte.Parse(re_d, System.Globalization.NumberStyles.HexNumber);
                var green = byte.Parse(g_reen, System.Globalization.NumberStyles.HexNumber);
                var blue = byte.Parse(_blue, System.Globalization.NumberStyles.HexNumber);
                _spriteBatch.Draw(pixel, position, new Color(red, green, blue));
            }
            _spriteBatch.End();

            // after drawing the game at native resolution we can render _nativeRenderTarget to the backbuffer!
            // First set the GraphicsDevice target back to the backbuffer
            GraphicsDevice.SetRenderTarget(null);
            // RenderTarget2D inherits from Texture2D so we can render it just like a texture
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_nativeRenderTarget, new Rectangle(x: 0, y: 0, width: (int)Math.Round(pc.screenScale * pc.screenWidth), height: (int)Math.Round(pc.screenScale * pc.screenHeight)), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
