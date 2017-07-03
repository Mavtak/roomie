using System.Collections.Generic;

namespace Roomie.Common.Color
{
    public class ColorNamesDictionary
    {
        private readonly Dictionary<string, RgbColor> _nameToColor;
        private readonly Dictionary<RgbColor, List<string>> _colorToName;

        public ColorNamesDictionary()
        {
            _nameToColor = new Dictionary<string, RgbColor>();
            _colorToName = new Dictionary<RgbColor, List<string>>();

            Populate();
        }

        public RgbColor Find(string name)
        {
            if (name == null)
            {
                return null;
            }

            name = name.ToLower();

            if (!_nameToColor.ContainsKey(name))
            {
                return null;
            }

            var result = _nameToColor[name];

            return result;
        }

        public IEnumerable<string> Find(RgbColor color)
        {
            if (color == null)
            {
                return null;
            }

            if (!_colorToName.ContainsKey(color))
            {
                return new string[0];
            }

            var result = _colorToName[color];

            return result;
        }

        private void Add(string name, string value)
        {
            var color = ColorExtensions.FromHexString(value);
            var rgbColor = color.RedGreenBlue;

            Add(name, rgbColor);
        }

        private void Add(string name, RgbColor color)
        {
            name = name.ToLower();

            _nameToColor.Add(name, color);

            List<string> namesForThisColor;

            if (_colorToName.ContainsKey(color))
            {
                namesForThisColor = _colorToName[color];
            }
            else
            {
                namesForThisColor = new List<string>();
                _colorToName.Add(color, namesForThisColor);
            }

            namesForThisColor.Add(name);
        }

        private void Populate()
        {
            // from http://www.w3schools.com/cssref/css_colornames.asp
            Add("AliceBlue", "#F0F8FF");
            Add("AntiqueWhite", "#FAEBD7");
            Add("Aqua", "#00FFFF");
            Add("Aquamarine", "#7FFFD4");
            Add("Azure", "#F0FFFF");
            Add("Beige", "#F5F5DC");
            Add("Bisque", "#FFE4C4");
            Add("Black", "#000000");
            Add("BlanchedAlmond", "#FFEBCD");
            Add("Blue", "#0000FF");
            Add("BlueViolet", "#8A2BE2");
            Add("Brown", "#A52A2A");
            Add("BurlyWood", "#DEB887");
            Add("CadetBlue", "#5F9EA0");
            Add("Chartreuse", "#7FFF00");
            Add("Chocolate", "#D2691E");
            Add("Coral", "#FF7F50");
            Add("CornflowerBlue", "#6495ED");
            Add("Cornsilk", "#FFF8DC");
            Add("Crimson", "#DC143C");
            Add("Cyan", "#00FFFF");
            Add("DarkBlue", "#00008B");
            Add("DarkCyan", "#008B8B");
            Add("DarkGoldenRod", "#B8860B");
            Add("DarkGray", "#A9A9A9");
            Add("DarkGreen", "#006400");
            Add("DarkKhaki", "#BDB76B");
            Add("DarkMagenta", "#8B008B");
            Add("DarkOliveGreen", "#556B2F");
            Add("DarkOrange", "#FF8C00");
            Add("DarkOrchid", "#9932CC");
            Add("DarkRed", "#8B0000");
            Add("DarkSalmon", "#E9967A");
            Add("DarkSeaGreen", "#8FBC8F");
            Add("DarkSlateBlue", "#483D8B");
            Add("DarkSlateGray", "#2F4F4F");
            Add("DarkTurquoise", "#00CED1");
            Add("DarkViolet", "#9400D3");
            Add("DeepPink", "#FF1493");
            Add("DeepSkyBlue", "#00BFFF");
            Add("DimGray", "#696969");
            Add("DodgerBlue", "#1E90FF");
            Add("FireBrick", "#B22222");
            Add("FloralWhite", "#FFFAF0");
            Add("ForestGreen", "#228B22");
            Add("Fuchsia", "#FF00FF");
            Add("Gainsboro", "#DCDCDC");
            Add("GhostWhite", "#F8F8FF");
            Add("Gold", "#FFD700");
            Add("GoldenRod", "#DAA520");
            Add("Gray", "#808080");
            Add("Green", "#008000");
            Add("GreenYellow", "#ADFF2F");
            Add("HoneyDew", "#F0FFF0");
            Add("HotPink", "#FF69B4");
            Add("IndianRed ", "#CD5C5C");
            Add("Indigo ", "#4B0082");
            Add("Ivory", "#FFFFF0");
            Add("Khaki", "#F0E68C");
            Add("Lavender", "#E6E6FA");
            Add("LavenderBlush", "#FFF0F5");
            Add("LawnGreen", "#7CFC00");
            Add("LemonChiffon", "#FFFACD");
            Add("LightBlue", "#ADD8E6");
            Add("LightCoral", "#F08080");
            Add("LightCyan", "#E0FFFF");
            Add("LightGoldenRodYellow", "#FAFAD2");
            Add("LightGray", "#D3D3D3");
            Add("LightGreen", "#90EE90");
            Add("LightPink", "#FFB6C1");
            Add("LightSalmon", "#FFA07A");
            Add("LightSeaGreen", "#20B2AA");
            Add("LightSkyBlue", "#87CEFA");
            Add("LightSlateGray", "#778899");
            Add("LightSteelBlue", "#B0C4DE");
            Add("LightYellow", "#FFFFE0");
            Add("Lime", "#00FF00");
            Add("LimeGreen", "#32CD32");
            Add("Linen", "#FAF0E6");
            Add("Magenta", "#FF00FF");
            Add("Maroon", "#800000");
            Add("MediumAquaMarine", "#66CDAA");
            Add("MediumBlue", "#0000CD");
            Add("MediumOrchid", "#BA55D3");
            Add("MediumPurple", "#9370DB");
            Add("MediumSeaGreen", "#3CB371");
            Add("MediumSlateBlue", "#7B68EE");
            Add("MediumSpringGreen", "#00FA9A");
            Add("MediumTurquoise", "#48D1CC");
            Add("MediumVioletRed", "#C71585");
            Add("MidnightBlue", "#191970");
            Add("MintCream", "#F5FFFA");
            Add("MistyRose", "#FFE4E1");
            Add("Moccasin", "#FFE4B5");
            Add("NavajoWhite", "#FFDEAD");
            Add("Navy", "#000080");
            Add("OldLace", "#FDF5E6");
            Add("Olive", "#808000");
            Add("OliveDrab", "#6B8E23");
            Add("Orange", "#FFA500");
            Add("OrangeRed", "#FF4500");
            Add("Orchid", "#DA70D6");
            Add("PaleGoldenRod", "#EEE8AA");
            Add("PaleGreen", "#98FB98");
            Add("PaleTurquoise", "#AFEEEE");
            Add("PaleVioletRed", "#DB7093");
            Add("PapayaWhip", "#FFEFD5");
            Add("PeachPuff", "#FFDAB9");
            Add("Peru", "#CD853F");
            Add("Pink", "#FFC0CB");
            Add("Plum", "#DDA0DD");
            Add("PowderBlue", "#B0E0E6");
            Add("Purple", "#800080");
            Add("Red", "#FF0000");
            Add("RosyBrown", "#BC8F8F");
            Add("RoyalBlue", "#4169E1");
            Add("SaddleBrown", "#8B4513");
            Add("Salmon", "#FA8072");
            Add("SandyBrown", "#F4A460");
            Add("SeaGreen", "#2E8B57");
            Add("SeaShell", "#FFF5EE");
            Add("Sienna", "#A0522D");
            Add("Silver", "#C0C0C0");
            Add("SkyBlue", "#87CEEB");
            Add("SlateBlue", "#6A5ACD");
            Add("SlateGray", "#708090");
            Add("Snow", "#FFFAFA");
            Add("SpringGreen", "#00FF7F");
            Add("SteelBlue", "#4682B4");
            Add("Tan", "#D2B48C");
            Add("Teal", "#008080");
            Add("Thistle", "#D8BFD8");
            Add("Tomato", "#FF6347");
            Add("Turquoise", "#40E0D0");
            Add("Violet", "#EE82EE");
            Add("Wheat", "#F5DEB3");
            Add("White", "#FFFFFF");
            Add("WhiteSmoke", "#F5F5F5");
            Add("Yellow", "#FFFF00");
            Add("YellowGreen", "#9ACD32");
        }
    }
}
