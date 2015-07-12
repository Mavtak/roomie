namespace Roomie.Web.Persistence.Helpers.Secrets
{
    public class BCryptSecret : ISecret
    {
        public const string Name = "bcrypt";

        private string _hash;

        public BCryptSecret(string hash)
        {
            _hash = hash;
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
                return _hash;
            }
        }

        public bool Verify(string value)
        {
            var result = BCrypt.Net.BCrypt.Verify(value, _hash);

            return result;
        }

        public static BCryptSecret FromPassword(string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            var result = new BCryptSecret(hash);

            return result;
        }
    }
}