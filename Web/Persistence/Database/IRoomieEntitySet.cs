using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Web.Persistence.Database
{
    public interface IRoomieEntitySet<TEntityType> : IQueryable<TEntityType>
        where TEntityType : class
    {
        void Add(TEntityType entity);
        void Remove(TEntityType entity);
        TEntityType Find(object id);
    }
}
