namespace Roomie.Web.Persistence.Helpers.Secrets
{
    public interface ISecret
    {
        string Name { get; }
        string StoredValue { get; }
        bool Verify(string value);
    }
}
