using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CookPC.VM;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CookPC {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int loopCounter = 0;
        // TODO: Get this from the cookpc filesystem
        private string bootScript = 
@"mcalloc 0
mcalloc 0
mcset 0
debug ""fard lol""
jump 3";
        private int maxInstruction;
        private int currentInstruction = 0;
        private string instruction;
        private List<Dictionary<string, dynamic>> memory = new List<Dictionary<string, dynamic>>();
        private int currentChunk;
        private int variableCount = 0;
        // TODO: Test it on android and windows
        string cookfolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/cookpc";
        
        Computer pc;
        Texture2D lelclub;
        RenderTarget2D _nativeRenderTarget;

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
	""instructionsPerFrame"": 750,
	""totalVariableCountLimit"": 5120,
	""bootSize"": 69,
	""localstorageSize"": 69,
	""floppySize"": 69,
	""screenWidth"": 640,
	""screenHeight"": 480
}");
                string pcJson = File.ReadAllText(cookfolder + "/cookpc.json");
                pc = JsonConvert.DeserializeObject<Computer>(pcJson);

                // TODO: change drive size
                File.WriteAllText(cookfolder + "/boot", new string(' ', pc.bootSize));
                File.WriteAllText(cookfolder + "/localstorage", new string(' ', pc.localstorageSize));
                File.WriteAllText(cookfolder + "/floppy_a", new string(' ', pc.floppySize));
                File.WriteAllText(cookfolder + "/floppy_b", new string(' ', pc.floppySize));
            } else {
                string pcJson = File.ReadAllText(cookfolder + "/cookpc.json");
                pc = JsonConvert.DeserializeObject<Computer>(pcJson);
            }

            // Set screen size
            _graphics.PreferredBackBufferWidth = pc.screenWidth;
            _graphics.PreferredBackBufferHeight = pc.screenHeight;
            
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            using (var fs = File.OpenRead("/home/toddynho/GameProjects/CookPC/SourceCode/CookPC/Content/_lelcube.png"))
                lelclub = Texture2D.FromStream(GraphicsDevice, fs);
        }

        protected override void Update(GameTime gameTime) {
            // Run stuff :)
            while (loopCounter < pc.instructionsPerFrame) {
                var jsssjjsjshshsj = Lexer.Tokenize(instruction, memory, currentChunk);
                /*foreach (var item in jsssjjsjshshsj) {
                    System.Console.WriteLine(item);
                }*/
                (memory, currentChunk, variableCount, currentInstruction) = InstructionRunner.Run(jsssjjsjshshsj, memory, currentChunk, variableCount, currentInstruction, cookfolder, pc);

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
            GraphicsDevice.Clear(Color.IndianRed);

            _spriteBatch.Begin();
            _spriteBatch.Draw(lelclub, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            // after drawing the game at native resolution we can render _nativeRenderTarget to the backbuffer!
            // First set the GraphicsDevice target back to the backbuffer
            GraphicsDevice.SetRenderTarget(null);
            // RenderTarget2D inherits from Texture2D so we can render it just like a texture
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_nativeRenderTarget, new Rectangle(x: 0, y: 0, width: 2 * pc.screenWidth, height: 2 * pc.screenHeight), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
