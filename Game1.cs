using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CookPC.VM;
using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

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
mvdef c 0
add $c 1 c
debug $c
equal $c 10 urmom
not $urmom urmom
ifjump 4 $urmom";
        private int maxInstruction;
        private int currentInstruction = 0;
        private string instruction;
        private List<Dictionary<string, dynamic>> memory = new List<Dictionary<string, dynamic>>();
        private int currentChunk;
        private int variableCount = 0;

        public Game1() {
            maxInstruction = bootScript.Split("\n").Length;
            instruction = bootScript.Split("\n")[currentInstruction];

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // Init filesystem
            // TODO: Test it on windows and android this sounds weird "and an-droid"
            string cookstorage = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/cookpc.xml"; // Thank you, Microsoft
            if (!File.Exists(cookstorage)) {
                File.WriteAllText(cookstorage,
@"<cookpc>

    <computer>
        <cpuArchitecture>leg16</cpuArchitecture>
        <varLimit>2048</varLimit>
        <!-- what kind of device is running cookpc -->
        <hostType>desktop</hostType>
        <screenX>640</screenX>
        <screenY>480</screenY>
        <!-- TODO: Add a color palette-->
        <soundChannels>16</soundChannels>
        <cookPcVersion>0.0.69</cookPcVersion>
        <!-- DEV: Development version, very unstable -->
        <!-- BETA: Beta -->
        <!-- Release: Will be used once 1.0 is out-->
        <cookPcReleaseType>DEV</cookPcReleaseType>
    </computer>

    <fsroot>
        <folder name=""system"">
            <file name=""bloat1"">ur</file>
            <file name=""bloat2"">mom</file>
            <file name=""bloat3"">is</file>
            <file name=""bloat4"">veri</file>
            <file name=""bloat5"">fat</file>
        </folder>
    </fsroot>

</cookpc>");
            }
            // TODO: Add an update system

            // Load the XML document
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText(cookstorage));

            XmlNode root = doc.FirstChild;
            if (root.HasChildNodes) {
                for (int i = 0; i < root.ChildNodes.Count; i++) {
                    for (int iphone = 0; iphone < root.ChildNodes[i].ChildNodes.Count; iphone++) {
                        Console.WriteLine(root.ChildNodes[i].ChildNodes[iphone].InnerText);
                    }
                }
            }

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
                (memory, currentChunk, variableCount, currentInstruction) = InstructionRunner.Run(jsssjjsjshshsj, memory, currentChunk, variableCount, currentInstruction);

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
