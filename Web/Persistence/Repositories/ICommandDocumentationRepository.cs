using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ICommandDocumentationRepository
    {
        Command[] Get();
    }
}
