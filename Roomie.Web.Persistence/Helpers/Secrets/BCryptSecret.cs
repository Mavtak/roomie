using DevOne.Security.Cryptography.BCrypt;

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
            var result = BCryptHelper.CheckPassword(value, _hash);

            return result;
        }

        public static BCryptSecret FromPassword(string password)
        {
            var salt = BCryptHelper.GenerateSalt();
            var hash = BCryptHelper.HashPassword(password, salt);
            var result = new BCryptSecret(hash);

            return result;
        }
    }
}