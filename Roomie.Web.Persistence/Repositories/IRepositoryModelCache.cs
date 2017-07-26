namespace Roomie.Web.Persistence.Repositories
{
    public interface IRepositoryModelCache
    {
        TModel Get<TModel>(object id)
            where TModel : class;
        void Set<TModel>(object id, TModel model)
            where TModel : class;
    }
}
