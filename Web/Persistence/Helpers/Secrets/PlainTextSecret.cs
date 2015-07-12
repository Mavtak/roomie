namespace Roomie.Web.Persistence.Helpers.Secrets
{
    public class PlainTextSecret : ISecret
    {
        public const string Name = "plain-text";

        private string _password;

        public PlainTextSecret(string value)
        {
            _password = value;
        }

        string ISecret.Name
        {
            get
            {
                return Name;
            }
        }

        public string StoredValue
        {
            get
            {
                return _password;
            }
        }

        public bool Verify(string value)
        {
            var result = string.Equals(_password, value);

            return result;
        }
    }
}