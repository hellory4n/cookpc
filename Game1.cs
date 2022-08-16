﻿using Microsoft.Xna.Framework;
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
debug ""e""
jump -1";
        private int maxInstruction;
        private int currentInstruction = 0;
        private string instruction;
        private List<Dictionary<string, dynamic>> memory = new List<Dictionary<string, dynamic>>();
        private int currentChunk;
        private int variableCount = 0;
        // TODO: Test it on android and windows
        string cookfolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/cookpc";

        public Game1() {
            maxInstruction = bootScript.Split("\n").Length;
            instruction = bootScript.Split("\n")[currentInstruction];

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            if (!Directory.Exists(cookfolder)) {
                Directory.CreateDirectory(cookfolder);
                File.WriteAllText(cookfolder + "/drives.json", 
@"{
	""localstorage"": {
		""max"": 69,
		""partitions"": [
			{
				""max"": 69
			}
		]
	},
	""floppy_a"": {
		""max"": 69,
		""partitions"": [
			{
				""max"": 69
			}
		]
	},
	""floppy_b"": {
		""max"": 69,
		""partitions"": [
			{
				""max"": 69
			}
		]
	}
}");
                // TODO: change drive size
                File.WriteAllText(cookfolder + "/localstorage1", new string(' ', 69));
                File.WriteAllText(cookfolder + "/floppy_a1", new string(' ', 69));
                File.WriteAllText(cookfolder + "/floppy_b1", new string(' ', 69));
            }

            // string deviceInfoJson = File.ReadAllText(cookfolder + "/drives.json");
            // deviceInfo = JsonConvert.DeserializeObject<Devices>(deviceInfoJson);

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            // Run stuff :)
            // TODO: Make it blazingly fast
            while (loopCounter < 1) {
                var jsssjjsjshshsj = Lexer.Tokenize(instruction, memory, currentChunk);
                /*foreach (var item in jsssjjsjshshsj) {
                    System.Console.WriteLine(item);
                }*/
                (memory, currentChunk, variableCount, currentInstruction) = InstructionRunner.Run(jsssjjsjshshsj, memory, currentChunk, variableCount, currentInstruction, cookfolder);

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
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
