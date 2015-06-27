
namespace Roomie.Desktop.Engine.Parameters
{
    public class VariableParameter : IParameter
    {
        private readonly string _name;
        private string _value;

        public VariableParameter(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public void Update(string value)
        {
            _value = value;
        }

        #region IParameter (with Value setter)

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
    }
}
