
namespace Roomie.Desktop.Engine.Parameters
{
    public class ReadOnlyParameter : IParameter
    {
        private readonly string _name;
        private readonly string _value;

        public ReadOnlyParameter(string name, string value)
        {
            _name = name;
            _value = value;
        }

        #region IParameter

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }
        }

        #endregion

        #region Object

        public override string ToString()
        {
            return this.Format();
        }

        #endregion
    }
}
