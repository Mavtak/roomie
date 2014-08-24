namespace Roomie.Common.Color
{
    public class NamedColor : IColor
    {
        private readonly string _name;

        public NamedColor(string name)
        {
            _name = name;
        }

        public string Value
        {
            get
            {
                return _name;
            }
        }

        #region IColor implementation

        public NamedColor Name
        {
            get { return this; }
        }

        public RgbColor RedGreenBlue
        {
            get
            {
                var result = Utilities.NameDictionary.Find(_name);

                return result;
            }
        }

        #endregion
    }
}
