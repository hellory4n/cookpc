using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CookPC.VM;
using System;
using System.Collections.Generic;

namespace CookPC {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int loopCounter = 0;
        // TODO: Get this from the cookpc filesystem
        private string bootScript = 
@"mcalloc 0
mcset 0
mvdef epokvariable 69
mvdef var true
mvdef string ""sussy amogus""";
        private int maxInstruction;
        private int currentInstruction = 0;
        private string instruction;
        private List<Dictionary<string, dynamic>> memory = new List<Dictionary<string, dynamic>>();
        private int currentChunk;

        public Game1() {
            maxInstruction = bootScript.Split("\n").Length;
            instruction = bootScript.Split("\n")[currentInstruction];

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            while (loopCounter < 5) {
                var jsssjjsjshshsj = Lexer.Tokenize(instruction);
                /*foreach (var item in jsssjjsjshshsj) {
                    System.Console.WriteLine(item);
                }*/
                (memory, currentChunk) = InstructionRunner.Run(jsssjjsjshshsj, memory, currentChunk);

                currentInstruction++;
                if (currentInstruction == maxInstruction)
                    currentInstruction = 0;

                instruction = bootScript.Split("\n")[currentInstruction];


                loopCounter++;
            }

            foreach (KeyValuePair<string, dynamic> m in memory[currentChunk]) {
                System.Console.WriteLine(m.Key + ": " + m.Value);
            }

            // TODO: Uncomment this
            // loopCounter = 0;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
