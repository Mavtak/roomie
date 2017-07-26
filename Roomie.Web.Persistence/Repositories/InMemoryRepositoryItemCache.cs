using System;
using System.Collections.Generic;

namespace Roomie.Web.Persistence.Repositories
{
    public class InMemoryRepositoryModelCache : IRepositoryModelCache
    {
        private Dictionary<Type, Dictionary<object, object>> _cache;

        public InMemoryRepositoryModelCache()
        {
            _cache = new Dictionary<Type, Dictionary<object, object>>();
        }

        public TModel Get<TModel>(object id)
            where TModel : class
        {
            if (!_cache.ContainsKey(typeof(TModel)))
            {
                return null;
            }

            var repositoryModels = _cache[typeof(TModel)];

            if (!repositoryModels.ContainsKey(id))
            {
                return null;
            }

            var model = (TModel)repositoryModels[id];

            return model;
        }

        public void Set<TModel>(object id, TModel model)
            where TModel : class
        {
            if (!_cache.ContainsKey(typeof(TModel)))
            {
                _cache.Add(typeof(TModel), new Dictionary<object, object>());
            }

            var repositoryModels = _cache[typeof(TModel)];

            if (!repositoryModels.ContainsKey(id))
            {
                repositoryModels.Add(id, model);
            }
            else
            {
                repositoryModels[id] = model;
            }
        }
    }
}
