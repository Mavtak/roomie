using System.Linq;
using System.Text;

namespace Roomie.Common.Color
{
    public class RgbColor : IColor
    {
        public byte Red { get; private set; }
        public byte Green { get; private set; }
        public byte Blue { get; private set; }

        public RgbColor(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        #region IColor implementation

        public NamedColor Name
        {
            get
            {
                var values = Utilities.NameDictionary.Find(this);
                var result = new NamedColor(values.FirstOrDefault());

                return result;
            }
        }

        public RgbColor RedGreenBlue
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region object overrides and helpers

        public override int GetHashCode()
        {
            // ReSharper, you so smart!
            unchecked
            {
                var result = Red.GetHashCode();
                result = (result*397) ^ Green.GetHashCode();
                result = (result*397) ^ Blue.GetHashCode();

                return result;
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as RgbColor;

            if (other == null)
            {
                return false;
            }

            return Equals(other);
        }

        public bool Equals(RgbColor other)
        {
            if (Red != other.Red)
            {
                return false;
            }

            if (Green != other.Green)
            {
                return false;
            }

            if (Blue != other.Blue)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.Append("rgb(");
            result.Append(Red);
            result.Append(", ");
            result.Append(Green);
            result.Append(", ");
            result.Append(Blue);
            result.Append(")");

            return result.ToString();
        }

        #endregion
    }
}
