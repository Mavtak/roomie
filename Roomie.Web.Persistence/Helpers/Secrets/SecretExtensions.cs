using System.Text;

namespace Roomie.Web.Persistence.Helpers.Secrets
{
    public static class SecretExtensions
    {
        public static string Format(this ISecret secret)
        {
            var result = new StringBuilder();

            result.Append(secret.Name);
            result.Append(":");
            result.Append(secret.StoredValue);

            return result.ToString();
        }

        public static ISecret Parse(string format)
        {
            if (string.IsNullOrEmpty(format) || string.IsNullOrEmpty(format))
            {
                return null;
            }

            var index = format.IndexOf(':');

            if (index < 0)
            {
                return null;
            }

            var type = format.Substring(0, index);
            var value = format.Substring(index + 1);
            var result = CreateSecret(type, value);

            return result;
        }

        public static ISecret CreateSecret(string type, string value)
        {
            ISecret result;

            switch (type)
            {
                case PlainTextSecret.Name:
                    result = new PlainTextSecret(value);
                    break;

                case BCryptSecret.Name:
                    result = new BCryptSecret(value);
                    break;

                default:
                    result = null;
                    break;
            }

            return result;
        }
    }
}
