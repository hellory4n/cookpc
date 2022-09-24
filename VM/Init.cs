using System;
using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

namespace CookPC.VM {
    class Init {
        public static (Color[], Settings) CookPcInit() {
            #region Load/create the color palette file
            #region rider can't minimize this massive string so i made this region
            const string originalPalette = 
@"0,0,0
11,11,11
34,34,34
68,68,68
85,85,85
119,119,119
136,136,136
170,170,170
187,187,187
221,221,221
238,238,238
0,0,11
0,0,34
0,0,68
0,0,85
0,0,119
0,0,136
0,0,170
0,0,187
0,0,221
0,0,238
0,11,0
0,34,0
0,68,0
0,85,0
0,119,0
0,136,0
0,170,0
0,187,0
0,221,0
0,238,0
11,0,0
34,0,0
68,0,0
85,0,0
119,0,0
136,0,0
170,0,0
187,0,0
221,0,0
238,0,0
0,0,51
0,0,102
0,0,153
0,0,204
0,0,255
0,51,0
0,51,51
0,51,102
0,51,153
0,51,204
0,51,255
0,102,0
0,102,51
0,102,102
0,102,153
0,102,204
0,102,255
0,153,0
0,153,51
0,153,102
0,153,153
0,153,204
0,153,255
0,204,0
0,204,51
0,204,102
0,204,153
0,204,204
0,204,255
0,255,0
0,255,51
0,255,102
0,255,153
0,255,204
0,255,255
51,0,0
51,0,51
51,0,102
51,0,153
51,0,204
51,0,255
51,51,0
51,51,51
51,51,102
51,51,153
51,51,204
51,51,255
51,102,0
51,102,51
51,102,102
51,102,153
51,102,204
51,102,255
51,153,0
51,153,51
51,153,102
51,153,153
51,153,204
51,153,255
51,204,0
51,204,51
51,204,102
51,204,153
51,204,204
51,204,255
51,255,0
51,255,51
51,255,102
51,255,153
51,255,204
51,255,255
102,0,0
102,0,51
102,0,102
102,0,153
102,0,204
102,0,255
102,51,0
102,51,51
102,51,102
102,51,153
102,51,204
102,51,255
102,102,0
102,102,51
102,102,102
102,102,153
102,102,204
102,102,255
102,153,0
102,153,51
102,153,102
102,153,153
102,153,204
102,153,255
102,204,0
102,204,51
102,204,102
102,204,153
102,204,204
102,204,255
102,255,0
102,255,51
102,255,102
102,255,153
102,255,204
102,255,255
153,0,0
153,0,51
153,0,102
153,0,153
153,0,204
153,0,255
153,51,0
153,51,51
153,51,102
153,51,153
153,51,204
153,51,255
153,102,0
153,102,51
153,102,102
153,102,153
153,102,204
153,102,255
153,153,0
153,153,51
153,153,102
153,153,153
153,153,204
153,153,255
153,204,0
153,204,51
153,204,102
153,204,153
153,204,204
153,204,255
153,255,0
153,255,51
153,255,102
153,255,153
153,255,204
153,255,255
204,0,0
204,0,51
204,0,102
204,0,153
204,0,204
204,0,255
204,51,0
204,51,51
204,51,102
204,51,153
204,51,204
204,51,255
204,102,0
204,102,51
204,102,102
204,102,153
204,102,204
204,102,255
204,153,0
204,153,51
204,153,102
204,153,153
204,153,204
204,153,255
204,204,0
204,204,51
204,204,102
204,204,153
204,204,204
204,204,255
204,255,0
204,255,51
204,255,102
204,255,153
204,255,204
204,255,255
255,0,0
255,0,51
255,0,102
255,0,153
255,0,204
255,0,255
255,51,0
255,51,51
255,51,102
255,51,153
255,51,204
255,51,255
255,102,0
255,102,51
255,102,102
255,102,153
255,102,204
255,102,255
255,153,0
255,153,51
255,153,102
255,153,153
255,153,204
255,153,255
255,204,0
255,204,51
255,204,102
255,204,153
255,204,204
255,204,255
255,255,0
255,255,51
255,255,102
255,255,153
255,255,204
255,255,255";
            #endregion 
            string palette = "";
            Godot.File colorFile = new Godot.File();
            if (!colorFile.FileExists("user://colors")) {
                colorFile.Open("user://colors", File.ModeFlags.Write);
                colorFile.StoreString(originalPalette);
                palette = originalPalette;
            } else {
                colorFile.Open("user://colors", File.ModeFlags.Read);
                palette = colorFile.GetAsText();
            }
            colorFile.Close();

            // Put those colors into color objects
            string[] lines = palette.Split("\n");
            List<Color> finalPalette = new List<Color>();
            foreach (var item in lines) {
                string[] rgb = item.Split(",");
                float red = float.Parse(rgb[0])/256;
                float green = float.Parse(rgb[1])/256;
                float blue = float.Parse(rgb[2])/256;
                finalPalette.Add(new Color(red, green, blue));
            }
            #endregion

            #region cookpc.json
            Godot.File cookPcJson = new Godot.File();
            Settings settings = new Settings();
            if (!cookPcJson.FileExists("user://cookpc.json")) {
                cookPcJson.Open("user://cookpc.json", File.ModeFlags.Write);
                string jason = JsonConvert.SerializeObject(settings);
                cookPcJson.StoreString(jason);
            } else {
                cookPcJson.Open("user://cookpc.json", File.ModeFlags.Read);
                string jayson = cookPcJson.GetAsText();
                settings = JsonConvert.DeserializeObject<Settings>(jayson);
            }
            #endregion

            return (finalPalette.ToArray(), settings);
        }
    }
}